using Microsoft.Extensions.DependencyInjection;

namespace Sharenv.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register application layers services to service collection
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection container)
        {
            container.AddSingleton<IExceptionManager, ExceptionManager>();

            return container;
        }
    }
}
