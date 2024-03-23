namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.GenericParameterMappingRepositoryFactoryCases.ParameterMappingCollectorCases;

using Moq;

using System.Collections.Generic;

internal sealed class CollectorContext<TParameter, TParameterRepresentation, TRecord, TData>
{
    public static CollectorContext<TParameter, TParameterRepresentation, TRecord, TData> Create()
    {
        ParameterMappingRepositoryFactory nonGenericFactory = new();

        Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> parameterRepresentationFactoryMock = new() { DefaultValue = DefaultValue.Mock };
        Mock<IEqualityComparer<TParameterRepresentation>> parameterComparerMock = new();

        var factory = ((IParameterMappingRepositoryFactory)nonGenericFactory).ForParameter(parameterRepresentationFactoryMock.Object, parameterComparerMock.Object);

        var repository = factory.Create<TRecord, TData>();

        return new(repository, parameterRepresentationFactoryMock, parameterComparerMock);
    }

    public IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TData> Repository { get; }
    public IParameterMappingCollector<TParameterRepresentation, TRecord, TData> Collector => Repository.Collector;

    public Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> ParameterRepresentationFactoryMock { get; }
    public Mock<IEqualityComparer<TParameterRepresentation>> ParameterComparerMock { get; }

    public CollectorContext(IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TData> repository, Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> parameterRepresentationFactoryMock, Mock<IEqualityComparer<TParameterRepresentation>> parameterComparerMock)
    {
        Repository = repository;

        ParameterRepresentationFactoryMock = parameterRepresentationFactoryMock;
        ParameterComparerMock = parameterComparerMock;
    }
}
