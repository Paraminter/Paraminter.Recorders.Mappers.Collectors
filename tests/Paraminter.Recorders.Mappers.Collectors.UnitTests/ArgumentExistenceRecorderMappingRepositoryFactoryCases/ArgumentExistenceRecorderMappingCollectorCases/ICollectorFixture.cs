namespace Paraminter.Recorders.Mappers.Collectors.ArgumentExistenceRecorderMappingRepositoryFactoryCases.ArgumentExistenceRecorderMappingCollectorCases;

using Moq;

using Paraminter.Parameters.Representations;

using System.Collections.Generic;

internal interface ICollectorFixture<TParameter, TParameterRepresentation, TRecord>
{
    public abstract IArgumentExistenceRecorderMappingCollector<TParameterRepresentation, TRecord> Sut { get; }

    public abstract IArgumentExistenceRecorderMappingRepository<TParameter, TParameterRepresentation, TRecord> Repository { get; }

    public abstract Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> ParameterRepresentationFactoryMock { get; }
    public abstract Mock<IEqualityComparer<TParameterRepresentation>> ParameterRepresentationComparerMock { get; }
}
