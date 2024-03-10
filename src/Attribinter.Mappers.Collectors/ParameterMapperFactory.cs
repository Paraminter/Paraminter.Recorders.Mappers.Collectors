namespace Attribinter.Mappers.Collectors;

using System;
using System.Collections.Generic;

/// <inheritdoc cref="IParameterMapperFactory"/>
public sealed class ParameterMapperFactory : IParameterMapperFactory
{
    private readonly IParameterMappingRepositoryFactory RepositoryFactory;

    /// <summary>Instantiates a <see cref="ParameterMapperFactory"/>, handling creation of <see cref="IParameterMapper{TParameter, TRecord, TData}"/>.</summary>
    /// <param name="repositoryFactory">Handles creation of <see cref="IParameterMappingRepository{TParameter, TRecord, TData}"/>.</param>
    public ParameterMapperFactory(IParameterMappingRepositoryFactory repositoryFactory)
    {
        RepositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
    }

    IParameterMapper<TParameter, TRecord, TData> IParameterMapperFactory.Create<TParameter, TRecord, TData>(IParameterMappingRegistrator<TParameter, TRecord, TData> registrator, IEqualityComparer<TParameter> parameterComparer)
    {
        if (registrator is null)
        {
            throw new ArgumentNullException(nameof(registrator));
        }

        if (parameterComparer is null)
        {
            throw new ArgumentNullException(nameof(parameterComparer));
        }

        var repository = RepositoryFactory.ForParameter(parameterComparer).Create<TRecord, TData>();

        registrator.Register(repository.Collector);

        return repository.Builder.Build();
    }
}
