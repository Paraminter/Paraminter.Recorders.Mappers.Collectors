namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.GenericParameterMappingRepositoryFactoryCases.ParameterMappingRepositoryCases;

using Moq;

using System.Collections.Generic;

internal sealed class RepositoryContext<TParameter, TRecord, TData>
{
    public static RepositoryContext<TParameter, TRecord, TData> Create()
    {
        ParameterMappingRepositoryFactory nonGenericFactory = new();

        var parameterComparer = Mock.Of<IEqualityComparer<TParameter>>();

        var genericFactory = ((IParameterMappingRepositoryFactory)nonGenericFactory).ForParameter(parameterComparer);

        var repository = genericFactory.Create<TRecord, TData>();

        return new(repository);
    }

    public IParameterMappingRepository<TParameter, TRecord, TData> Repository { get; }

    public RepositoryContext(IParameterMappingRepository<TParameter, TRecord, TData> repository)
    {
        Repository = repository;
    }
}
