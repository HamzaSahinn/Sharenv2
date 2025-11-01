using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Serilog;
using Sharenv.Application;
using Sharenv.Application.Configurations;
using Sharenv.Application.Extensions;
using Sharenv.Infra.Extensions;

namespace Sharenv.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var authConfig = builder.Configuration.GetRequiredSection(AuthConfiguration.SECTION_NAME).Get<AuthConfiguration>();
            var dbConfig = builder.Configuration.GetRequiredSection(DbConfiguration.SECTION_NAME).Get<DbConfiguration>();

            builder.Host.UseSerilog();

            builder.Services.AddSingleton<IExceptionManager>(new ExceptionManager());

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "SchemaRouter";
                options.DefaultChallengeScheme = "SchemaRouter";
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddPolicyScheme("SchemaRouter", "SchemaRouter", options =>
            {
                options.ForwardDefaultSelector = (context) =>
                {
                    string authorization = context.Request.Headers[HeaderNames.Authorization];
                    if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer "))
                    {
                        return JwtBearerDefaults.AuthenticationScheme;
                    }

                    return CookieAuthenticationDefaults.AuthenticationScheme;
                };
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = authConfig.JwtConfiguration.Authority;
                options.Audience = authConfig.JwtConfiguration.Audience;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudiences = authConfig.JwtConfiguration.ValidAudiences,
                    ValidIssuers = authConfig.JwtConfiguration.ValidIssuers
                };
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromSeconds(authConfig.CookieConfiguration.ExpireTimeInSec);
                options.LogoutPath = "/";
                options.LoginPath = "/Login";
                options.AccessDeniedPath = "/Unauthorized";
            });

            builder.Services.AddApplicationConfiguration(builder.Configuration)
                         .AddApplicationServices()
                         .AddInfraServices()
                         .AddControllers();

            var app = builder.Build();

            Core.RegisterLogger(app.Logger);
            Core.RegisterExceptionManager(app.Services.GetRequiredService<IExceptionManager>());

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
