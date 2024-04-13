namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.ParameterMappingCollectorCases;

using Attribinter.Parameters.Representations;

using Moq;

using System.Collections.Generic;

internal interface ICollectorFixture<TParameter, TParameterRepresentation, TRecord, TData>
{
    public abstract IParameterMappingCollector<TParameterRepresentation, TRecord, TData> Sut { get; }

    public abstract IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TData> Repository { get; }

    public abstract Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> ParameterRepresentationFactoryMock { get; }
    public abstract Mock<IEqualityComparer<TParameterRepresentation>> ParameterRepresentationComparerMock { get; }
}
