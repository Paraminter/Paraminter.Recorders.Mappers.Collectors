namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.ParameterMapperBuilderCases;

using Attribinter.Parameters.Representations;

using Moq;

using System.Collections.Generic;

internal static class BuilderFixtureFactory
{
    public static IBuilderFixture<TParameter, TParameterRepresentation, TRecord, TData> Create<TParameter, TParameterRepresentation, TRecord, TData>()
    {
        IParameterMappingRepositoryFactory factory = new ParameterMappingRepositoryFactory();

        Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> parameterRepresentationFactoryMock = new();
        Mock<IEqualityComparer<TParameterRepresentation>> parameterRepresentationComparerMock = new();

        var repository = factory.Create<TParameter, TParameterRepresentation, TRecord, TData>(parameterRepresentationFactoryMock.Object, parameterRepresentationComparerMock.Object);

        return new BuilderFixture<TParameter, TParameterRepresentation, TRecord, TData>(repository.Builder, parameterRepresentationFactoryMock, parameterRepresentationComparerMock);
    }

    private sealed class BuilderFixture<TParameter, TParameterRepresentation, TRecord, TData> : IBuilderFixture<TParameter, TParameterRepresentation, TRecord, TData>
    {
        private readonly IParameterMapperBuilder<TParameter, TRecord, TData> Sut;

        private readonly Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> ParameterRepresentationFactoryMock;
        private readonly Mock<IEqualityComparer<TParameterRepresentation>> ParameterRepresentationComparerMock;

        public BuilderFixture(IParameterMapperBuilder<TParameter, TRecord, TData> sut, Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> parameterRepresentationFactoryMock, Mock<IEqualityComparer<TParameterRepresentation>> parameterRepresentationComparerMock)
        {
            Sut = sut;

            ParameterRepresentationFactoryMock = parameterRepresentationFactoryMock;
            ParameterRepresentationComparerMock = parameterRepresentationComparerMock;
        }

        IParameterMapperBuilder<TParameter, TRecord, TData> IBuilderFixture<TParameter, TParameterRepresentation, TRecord, TData>.Sut => Sut;

        Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> IBuilderFixture<TParameter, TParameterRepresentation, TRecord, TData>.ParameterRepresentationFactoryMock => ParameterRepresentationFactoryMock;
        Mock<IEqualityComparer<TParameterRepresentation>> IBuilderFixture<TParameter, TParameterRepresentation, TRecord, TData>.ParameterRepresentationComparerMock => ParameterRepresentationComparerMock;
    }
}
