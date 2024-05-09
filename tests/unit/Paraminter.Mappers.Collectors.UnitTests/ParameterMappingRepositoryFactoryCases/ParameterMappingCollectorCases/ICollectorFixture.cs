namespace Paraminter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.ParameterMappingCollectorCases;

using Moq;

using Paraminter.Parameters.Representations;

using System.Collections.Generic;

internal interface ICollectorFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>
{
    public abstract IParameterMappingCollector<TParameterRepresentation, TRecord, TArgumentData> Sut { get; }

    public abstract IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TArgumentData> Repository { get; }

    public abstract Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> ParameterRepresentationFactoryMock { get; }
    public abstract Mock<IEqualityComparer<TParameterRepresentation>> ParameterRepresentationComparerMock { get; }
}
