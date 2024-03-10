namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.GenericParameterMappingRepositoryFactoryCases.ParameterMappingCollectorCases;

using Moq;

using System.Collections.Generic;

internal sealed class CollectorContext<TParameter, TRecord, TData>
{
    public static CollectorContext<TParameter, TRecord, TData> Create()
    {
        ParameterMappingRepositoryFactory nonGenericFactory = new();

        Mock<IEqualityComparer<TParameter>> parameterComparerMock = new();

        var factory = ((IParameterMappingRepositoryFactory)nonGenericFactory).ForParameter(parameterComparerMock.Object);

        var repository = factory.Create<TRecord, TData>();

        return new(repository, parameterComparerMock);
    }

    public IParameterMappingRepository<TParameter, TRecord, TData> Repository { get; }
    public IParameterMappingCollector<TParameter, TRecord, TData> Collector => Repository.Collector;

    public Mock<IEqualityComparer<TParameter>> ParameterComparerMock { get; }

    public CollectorContext(IParameterMappingRepository<TParameter, TRecord, TData> repository, Mock<IEqualityComparer<TParameter>> parameterComparerMock)
    {
        Repository = repository;

        ParameterComparerMock = parameterComparerMock;
    }
}
