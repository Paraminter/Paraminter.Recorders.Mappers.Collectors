namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.T2.ParameterMappingCollectorCases;

using Attribinter.Parameters.Representations;

using Moq;

using System.Collections.Generic;

internal sealed class CollectorContext<TParameter, TParameterRepresentation, TRecord, TData>
{
    public static CollectorContext<TParameter, TParameterRepresentation, TRecord, TData> Create()
    {
        IParameterMappingRepositoryFactory factory = new ParameterMappingRepositoryFactory();

        Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> parameterRepresentationFactoryMock = new() { DefaultValue = DefaultValue.Mock };
        Mock<IEqualityComparer<TParameterRepresentation>> parameterRepresentationComparerMock = new();

        var repository = factory.WithParameterRepresentation(parameterRepresentationFactoryMock.Object).Create<TRecord, TData>(parameterRepresentationComparerMock.Object);

        return new(repository, parameterRepresentationFactoryMock, parameterRepresentationComparerMock);
    }

    public IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TData> Repository { get; }
    public IParameterMappingCollector<TParameterRepresentation, TRecord, TData> Collector => Repository.Collector;

    public Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> ParameterRepresentationFactoryMock { get; }
    public Mock<IEqualityComparer<TParameterRepresentation>> ParameterRepresentationComparerMock { get; }

    public CollectorContext(IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TData> repository, Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> parameterRepresentationFactoryMock, Mock<IEqualityComparer<TParameterRepresentation>> parameterRepresentationComparerMock)
    {
        Repository = repository;

        ParameterRepresentationFactoryMock = parameterRepresentationFactoryMock;
        ParameterRepresentationComparerMock = parameterRepresentationComparerMock;
    }
}
