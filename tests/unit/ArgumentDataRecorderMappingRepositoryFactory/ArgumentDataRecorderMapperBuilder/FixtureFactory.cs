namespace Paraminter.Recorders.Mappers.Collectors.ArgumentDataRecorderMapperBuilder;

using Moq;

using Paraminter.Parameters.Representations;

using System.Collections.Generic;

internal static class FixtureFactory
{
    public static IFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData> Create<TParameter, TParameterRepresentation, TRecord, TArgumentData>()
    {
        IArgumentDataRecorderMappingRepositoryFactory factory = new ArgumentDataRecorderMappingRepositoryFactory();

        Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> parameterRepresentationFactoryMock = new() { DefaultValue = DefaultValue.Mock };
        Mock<IEqualityComparer<TParameterRepresentation>> parameterRepresentationComparerMock = new() { DefaultValue = DefaultValue.Mock };

        var repository = factory.Create<TParameter, TParameterRepresentation, TRecord, TArgumentData>(parameterRepresentationFactoryMock.Object, parameterRepresentationComparerMock.Object);

        return new Fixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>(repository.Builder, parameterRepresentationFactoryMock, parameterRepresentationComparerMock);
    }

    private sealed class Fixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>
        : IFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>
    {
        private readonly IArgumentDataRecorderMapperBuilder<TParameter, TRecord, TArgumentData> Sut;

        private readonly Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> ParameterRepresentationFactoryMock;
        private readonly Mock<IEqualityComparer<TParameterRepresentation>> ParameterRepresentationComparerMock;

        public Fixture(
            IArgumentDataRecorderMapperBuilder<TParameter, TRecord, TArgumentData> sut,
            Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> parameterRepresentationFactoryMock,
            Mock<IEqualityComparer<TParameterRepresentation>> parameterRepresentationComparerMock)
        {
            Sut = sut;

            ParameterRepresentationFactoryMock = parameterRepresentationFactoryMock;
            ParameterRepresentationComparerMock = parameterRepresentationComparerMock;
        }

        IArgumentDataRecorderMapperBuilder<TParameter, TRecord, TArgumentData> IFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>.Sut => Sut;

        Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> IFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>.ParameterRepresentationFactoryMock => ParameterRepresentationFactoryMock;
        Mock<IEqualityComparer<TParameterRepresentation>> IFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>.ParameterRepresentationComparerMock => ParameterRepresentationComparerMock;
    }
}
