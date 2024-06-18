namespace Paraminter.Recorders.Mappers.Collectors.ArgumentDataRecorderMapperBuilder;

using Moq;

using Paraminter.Parameters.Representations;

using System.Collections.Generic;

internal interface IFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>
{
    public abstract IArgumentDataRecorderMapperBuilder<TParameter, TRecord, TArgumentData> Sut { get; }

    public abstract Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> ParameterRepresentationFactoryMock { get; }
    public abstract Mock<IEqualityComparer<TParameterRepresentation>> ParameterRepresentationComparerMock { get; }
}
