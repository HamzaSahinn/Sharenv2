using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Sharenv.Application.Interfaces.Initializable;

public class HostedServiceIntialize : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IEnumerable<Type> _initializableConcreteTypes;

    public HostedServiceIntialize(IServiceProvider provider)
    {
        _serviceProvider = provider;


        var assembly = Assembly.GetExecutingAssembly();

        _initializableConcreteTypes = assembly.GetTypes()
            .Where(t =>
                t.IsClass &&
                !t.IsAbstract &&
                typeof(IInitializable).IsAssignableFrom(t)
            );
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var scopedProvider = scope.ServiceProvider;

        foreach (var concreteType in _initializableConcreteTypes)
        {
            var serviceInstance = scopedProvider.GetRequiredService(concreteType);

            if (serviceInstance is IInitializable initializableService)
            {
                initializableService.Initialize();
            }
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
