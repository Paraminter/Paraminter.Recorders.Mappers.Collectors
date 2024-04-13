namespace Attribinter.Mappers.Collectors;

using Microsoft.Extensions.DependencyInjection;

using System;

/// <summary>Allows the services of <i>Attribinter.Mappers.Collectors</i> to be registered with a <see cref="IServiceCollection"/>.</summary>
public static class AttribinterMapperCollectorsServices
{
    /// <summary>Registers the services of <i>Attribinter.Mappers.Collectors</i> with the provided <see cref="IServiceCollection"/>.</summary>
    /// <param name="services">The <see cref="IServiceCollection"/> with which services are registered.</param>
    /// <returns>The provided <see cref="IServiceCollection"/>, so that calls can be chained.</returns>
    public static IServiceCollection AddAttribinterMapperCollectors(this IServiceCollection services)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddTransient<IParameterMapperFactory, ParameterMapperFactory>();

        services.AddTransient<IParameterMappingRepositoryFactory, ParameterMappingRepositoryFactory>();

        return services;
    }
}
