namespace Paraminter.Recorders.Mappers.Collectors.ArgumentDataRecorderMappingRepositoryFactoryCases.ArgumentDataRecorderMapperBuilderCases;

using Moq;

using Paraminter.Parameters.Representations;

using System.Collections.Generic;

internal interface IBuilderFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>
{
    public abstract IArgumentDataRecorderMapperBuilder<TParameter, TRecord, TArgumentData> Sut { get; }

    public abstract Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> ParameterRepresentationFactoryMock { get; }
    public abstract Mock<IEqualityComparer<TParameterRepresentation>> ParameterRepresentationComparerMock { get; }
}
