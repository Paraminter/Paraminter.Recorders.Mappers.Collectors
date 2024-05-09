namespace Paraminter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.ParameterMapperBuilderCases;

using Moq;

using Paraminter.Parameters.Representations;

using System.Collections.Generic;

internal static class BuilderFixtureFactory
{
    public static IBuilderFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData> Create<TParameter, TParameterRepresentation, TRecord, TArgumentData>()
    {
        IParameterMappingRepositoryFactory factory = new ParameterMappingRepositoryFactory();

        Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> parameterRepresentationFactoryMock = new();
        Mock<IEqualityComparer<TParameterRepresentation>> parameterRepresentationComparerMock = new();

        var repository = factory.Create<TParameter, TParameterRepresentation, TRecord, TArgumentData>(parameterRepresentationFactoryMock.Object, parameterRepresentationComparerMock.Object);

        return new BuilderFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>(repository.Builder, parameterRepresentationFactoryMock, parameterRepresentationComparerMock);
    }

    private sealed class BuilderFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData> : IBuilderFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>
    {
        private readonly IParameterMapperBuilder<TParameter, TRecord, TArgumentData> Sut;

        private readonly Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> ParameterRepresentationFactoryMock;
        private readonly Mock<IEqualityComparer<TParameterRepresentation>> ParameterRepresentationComparerMock;

        public BuilderFixture(IParameterMapperBuilder<TParameter, TRecord, TArgumentData> sut, Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> parameterRepresentationFactoryMock, Mock<IEqualityComparer<TParameterRepresentation>> parameterRepresentationComparerMock)
        {
            Sut = sut;

            ParameterRepresentationFactoryMock = parameterRepresentationFactoryMock;
            ParameterRepresentationComparerMock = parameterRepresentationComparerMock;
        }

        IParameterMapperBuilder<TParameter, TRecord, TArgumentData> IBuilderFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>.Sut => Sut;

        Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> IBuilderFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>.ParameterRepresentationFactoryMock => ParameterRepresentationFactoryMock;
        Mock<IEqualityComparer<TParameterRepresentation>> IBuilderFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>.ParameterRepresentationComparerMock => ParameterRepresentationComparerMock;
    }
}
