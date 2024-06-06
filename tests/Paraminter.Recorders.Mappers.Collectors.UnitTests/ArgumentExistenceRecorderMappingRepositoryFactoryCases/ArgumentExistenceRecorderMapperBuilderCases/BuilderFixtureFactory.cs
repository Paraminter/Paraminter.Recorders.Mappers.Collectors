namespace Paraminter.Recorders.Mappers.Collectors.ArgumentExistenceRecorderMappingRepositoryFactoryCases.ArgumentExistenceRecorderMapperBuilderCases;

using Moq;

using Paraminter.Parameters.Representations;

using System.Collections.Generic;

internal static class BuilderFixtureFactory
{
    public static IBuilderFixture<TParameter, TParameterRepresentation, TRecord> Create<TParameter, TParameterRepresentation, TRecord>()
    {
        IArgumentExistenceRecorderMappingRepositoryFactory factory = new ArgumentExistenceRecorderMappingRepositoryFactory();

        Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> parameterRepresentationFactoryMock = new() { DefaultValue = DefaultValue.Mock };
        Mock<IEqualityComparer<TParameterRepresentation>> parameterRepresentationComparerMock = new() { DefaultValue = DefaultValue.Mock };

        var repository = factory.Create<TParameter, TParameterRepresentation, TRecord>(parameterRepresentationFactoryMock.Object, parameterRepresentationComparerMock.Object);

        return new BuilderFixture<TParameter, TParameterRepresentation, TRecord>(repository.Builder, parameterRepresentationFactoryMock, parameterRepresentationComparerMock);
    }

    private sealed class BuilderFixture<TParameter, TParameterRepresentation, TRecord>
        : IBuilderFixture<TParameter, TParameterRepresentation, TRecord>
    {
        private readonly IArgumentExistenceRecorderMapperBuilder<TParameter, TRecord> Sut;

        private readonly Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> ParameterRepresentationFactoryMock;
        private readonly Mock<IEqualityComparer<TParameterRepresentation>> ParameterRepresentationComparerMock;

        public BuilderFixture(
            IArgumentExistenceRecorderMapperBuilder<TParameter, TRecord> sut,
            Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> parameterRepresentationFactoryMock,
            Mock<IEqualityComparer<TParameterRepresentation>> parameterRepresentationComparerMock)
        {
            Sut = sut;

            ParameterRepresentationFactoryMock = parameterRepresentationFactoryMock;
            ParameterRepresentationComparerMock = parameterRepresentationComparerMock;
        }

        IArgumentExistenceRecorderMapperBuilder<TParameter, TRecord> IBuilderFixture<TParameter, TParameterRepresentation, TRecord>.Sut => Sut;

        Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> IBuilderFixture<TParameter, TParameterRepresentation, TRecord>.ParameterRepresentationFactoryMock => ParameterRepresentationFactoryMock;
        Mock<IEqualityComparer<TParameterRepresentation>> IBuilderFixture<TParameter, TParameterRepresentation, TRecord>.ParameterRepresentationComparerMock => ParameterRepresentationComparerMock;
    }
}
