namespace Paraminter.Recorders.Mappers.Collectors.ArgumentDataRecorderMappingRepositoryFactoryCases.ArgumentDataRecorderMapperBuilderCases;

using Moq;

using Paraminter.Parameters.Representations;

using System.Collections.Generic;

internal static class BuilderFixtureFactory
{
    public static IBuilderFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData> Create<TParameter, TParameterRepresentation, TRecord, TArgumentData>()
    {
        IArgumentDataRecorderMappingRepositoryFactory factory = new ArgumentDataRecorderMappingRepositoryFactory();

        Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> parameterRepresentationFactoryMock = new() { DefaultValue = DefaultValue.Mock };
        Mock<IEqualityComparer<TParameterRepresentation>> parameterRepresentationComparerMock = new() { DefaultValue = DefaultValue.Mock };

        var repository = factory.Create<TParameter, TParameterRepresentation, TRecord, TArgumentData>(parameterRepresentationFactoryMock.Object, parameterRepresentationComparerMock.Object);

        return new BuilderFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>(repository.Builder, parameterRepresentationFactoryMock, parameterRepresentationComparerMock);
    }

    private sealed class BuilderFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>
        : IBuilderFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>
    {
        private readonly IArgumentDataRecorderMapperBuilder<TParameter, TRecord, TArgumentData> Sut;

        private readonly Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> ParameterRepresentationFactoryMock;
        private readonly Mock<IEqualityComparer<TParameterRepresentation>> ParameterRepresentationComparerMock;

        public BuilderFixture(
            IArgumentDataRecorderMapperBuilder<TParameter, TRecord, TArgumentData> sut,
            Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> parameterRepresentationFactoryMock,
            Mock<IEqualityComparer<TParameterRepresentation>> parameterRepresentationComparerMock)
        {
            Sut = sut;

            ParameterRepresentationFactoryMock = parameterRepresentationFactoryMock;
            ParameterRepresentationComparerMock = parameterRepresentationComparerMock;
        }

        IArgumentDataRecorderMapperBuilder<TParameter, TRecord, TArgumentData> IBuilderFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>.Sut => Sut;

        Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> IBuilderFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>.ParameterRepresentationFactoryMock => ParameterRepresentationFactoryMock;
        Mock<IEqualityComparer<TParameterRepresentation>> IBuilderFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>.ParameterRepresentationComparerMock => ParameterRepresentationComparerMock;
    }
}
