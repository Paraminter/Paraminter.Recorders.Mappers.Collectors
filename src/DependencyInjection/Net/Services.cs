namespace Paraminter.Recorders.Mappers.Collectors;

using Microsoft.Extensions.DependencyInjection;

using System;

/// <summary>Allows the services provided by <i>Paraminter.Recorders.Mappers.Collectors</i> to be registered with a <see cref="IServiceCollection"/>.</summary>
public static class Services
{
    /// <summary>Registers the services provided by <i>Paraminter.Recorders.Mappers.Collectors</i> with the provided <see cref="IServiceCollection"/>.</summary>
    /// <param name="services">The <see cref="IServiceCollection"/> with which services are registered.</param>
    /// <returns>The provided <see cref="IServiceCollection"/>, so that calls can be chained.</returns>
    public static IServiceCollection AddParaminterRecorderMapperCollectors(this IServiceCollection services)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddTransient<IArgumentDataRecorderMapperFactory, ArgumentDataRecorderMapperFactory>();
        services.AddTransient<IArgumentExistenceRecorderMapperFactory, ArgumentExistenceRecorderMapperFactory>();

        services.AddTransient<IArgumentDataRecorderMappingRepositoryFactory, ArgumentDataRecorderMappingRepositoryFactory>();
        services.AddTransient<IArgumentExistenceRecorderMappingRepositoryFactory, ArgumentExistenceRecorderMappingRepositoryFactory>();

        return services;
    }
}
