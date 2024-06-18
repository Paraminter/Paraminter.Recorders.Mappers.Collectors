namespace Paraminter.Recorders.Mappers.Collectors.ArgumentExistenceRecorderMapperBuilder;

using Moq;

using Paraminter.Parameters.Representations;

using System.Collections.Generic;

internal static class FixtureFactory
{
    public static IFixture<TParameter, TParameterRepresentation, TRecord> Create<TParameter, TParameterRepresentation, TRecord>()
    {
        IArgumentExistenceRecorderMappingRepositoryFactory factory = new ArgumentExistenceRecorderMappingRepositoryFactory();

        Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> parameterRepresentationFactoryMock = new() { DefaultValue = DefaultValue.Mock };
        Mock<IEqualityComparer<TParameterRepresentation>> parameterRepresentationComparerMock = new() { DefaultValue = DefaultValue.Mock };

        var repository = factory.Create<TParameter, TParameterRepresentation, TRecord>(parameterRepresentationFactoryMock.Object, parameterRepresentationComparerMock.Object);

        return new Fixture<TParameter, TParameterRepresentation, TRecord>(repository.Builder, parameterRepresentationFactoryMock, parameterRepresentationComparerMock);
    }

    private sealed class Fixture<TParameter, TParameterRepresentation, TRecord>
        : IFixture<TParameter, TParameterRepresentation, TRecord>
    {
        private readonly IArgumentExistenceRecorderMapperBuilder<TParameter, TRecord> Sut;

        private readonly Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> ParameterRepresentationFactoryMock;
        private readonly Mock<IEqualityComparer<TParameterRepresentation>> ParameterRepresentationComparerMock;

        public Fixture(
            IArgumentExistenceRecorderMapperBuilder<TParameter, TRecord> sut,
            Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> parameterRepresentationFactoryMock,
            Mock<IEqualityComparer<TParameterRepresentation>> parameterRepresentationComparerMock)
        {
            Sut = sut;

            ParameterRepresentationFactoryMock = parameterRepresentationFactoryMock;
            ParameterRepresentationComparerMock = parameterRepresentationComparerMock;
        }

        IArgumentExistenceRecorderMapperBuilder<TParameter, TRecord> IFixture<TParameter, TParameterRepresentation, TRecord>.Sut => Sut;

        Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> IFixture<TParameter, TParameterRepresentation, TRecord>.ParameterRepresentationFactoryMock => ParameterRepresentationFactoryMock;
        Mock<IEqualityComparer<TParameterRepresentation>> IFixture<TParameter, TParameterRepresentation, TRecord>.ParameterRepresentationComparerMock => ParameterRepresentationComparerMock;
    }
}
