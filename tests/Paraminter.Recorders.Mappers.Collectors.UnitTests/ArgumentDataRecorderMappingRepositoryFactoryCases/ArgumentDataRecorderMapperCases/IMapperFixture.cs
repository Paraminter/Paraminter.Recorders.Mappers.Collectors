namespace Paraminter.Recorders.Mappers.Collectors.ArgumentDataRecorderMappingRepositoryFactoryCases.ArgumentDataRecorderMapperCases;

using Moq;

using Paraminter.Parameters.Representations;

using System.Collections.Generic;

internal interface IMapperFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>
{
    public abstract IArgumentDataRecorderMapper<TParameter, TRecord, TArgumentData> Sut { get; }

    public abstract Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> ParameterRepresentationFactoryMock { get; }
    public abstract Mock<IEqualityComparer<TParameterRepresentation>> ParameterRepresentationComparerMock { get; }
}
