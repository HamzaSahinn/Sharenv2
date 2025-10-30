using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sharenv.Application.Interfaces;
using Sharenv.Infra.Data;
using Sharenv.Infra.Service;

namespace Sharenv.Infra.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add db related registeries
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection container, IConfiguration configuration)
        {
            container.AddDbContext<SharenvDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            return container;
        }

        /// <summary>
        /// Register application layers services to service collection
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfraServices(this IServiceCollection container)
        {
            container.AddScoped<IActivityService, ActivityService>();
            container.AddScoped<ICircleService, CircleService>();
            container.AddScoped<ICircleMemberService, CircleMemberService>();
            container.AddScoped<IUserEntityService, UserService>();
            container.AddScoped<ICurrentUserService, CurrentUserService>();

            return container;
        }
    }
}
