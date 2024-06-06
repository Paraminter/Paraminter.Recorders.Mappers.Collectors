namespace Paraminter.Recorders.Mappers.Collectors.ArgumentDataRecorderMappingRepositoryFactoryCases.ArgumentDataRecorderMappingCollectorCases;

using Moq;

using Paraminter.Parameters.Representations;

using System.Collections.Generic;

internal interface ICollectorFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>
{
    public abstract IArgumentDataRecorderMappingCollector<TParameterRepresentation, TRecord, TArgumentData> Sut { get; }

    public abstract IArgumentDataRecorderMappingRepository<TParameter, TParameterRepresentation, TRecord, TArgumentData> Repository { get; }

    public abstract Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> ParameterRepresentationFactoryMock { get; }
    public abstract Mock<IEqualityComparer<TParameterRepresentation>> ParameterRepresentationComparerMock { get; }
}
