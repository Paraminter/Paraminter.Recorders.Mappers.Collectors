namespace Paraminter.Recorders.Mappers.Collectors.ArgumentExistenceRecorderMappingRepositoryFactoryCases.ArgumentExistenceRecorderMapperCases;

using Moq;

using Paraminter.Parameters.Representations;

using System.Collections.Generic;

internal interface IMapperFixture<TParameter, TParameterRepresentation, TRecord>
{
    public abstract IArgumentExistenceRecorderMapper<TParameter, TRecord> Sut { get; }

    public abstract Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> ParameterRepresentationFactoryMock { get; }
    public abstract Mock<IEqualityComparer<TParameterRepresentation>> ParameterRepresentationComparerMock { get; }
}
