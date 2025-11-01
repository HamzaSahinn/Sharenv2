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

            builder.Services.AddApplicationConfiguration(builder.Configuration)
                            .AddApplicationServices()
                            .AddInfraServices();

            builder.Services.AddControllers();

            var app = builder.Build();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
