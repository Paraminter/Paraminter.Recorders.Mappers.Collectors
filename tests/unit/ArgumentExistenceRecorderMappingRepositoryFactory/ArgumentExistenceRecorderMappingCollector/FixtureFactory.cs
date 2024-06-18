namespace Paraminter.Recorders.Mappers.Collectors.ArgumentExistenceRecorderMappingCollector;

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

        return new Fixture<TParameter, TParameterRepresentation, TRecord>(repository.Collector, repository, parameterRepresentationFactoryMock, parameterRepresentationComparerMock);
    }

    private sealed class Fixture<TParameter, TParameterRepresentation, TRecord>
        : IFixture<TParameter, TParameterRepresentation, TRecord>
    {
        private readonly IArgumentExistenceRecorderMappingCollector<TParameterRepresentation, TRecord> Sut;

        private readonly IArgumentExistenceRecorderMappingRepository<TParameter, TParameterRepresentation, TRecord> Repository;

        private readonly Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> ParameterRepresentationFactoryMock;
        private readonly Mock<IEqualityComparer<TParameterRepresentation>> ParameterRepresentationComparerMock;

        public Fixture(
            IArgumentExistenceRecorderMappingCollector<TParameterRepresentation, TRecord> sut,
            IArgumentExistenceRecorderMappingRepository<TParameter, TParameterRepresentation, TRecord> repository,
            Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> parameterRepresentationFactoryMock,
            Mock<IEqualityComparer<TParameterRepresentation>> parameterRepresentationComparerMock)
        {
            Sut = sut;

            Repository = repository;

            ParameterRepresentationFactoryMock = parameterRepresentationFactoryMock;
            ParameterRepresentationComparerMock = parameterRepresentationComparerMock;
        }

        IArgumentExistenceRecorderMappingCollector<TParameterRepresentation, TRecord> IFixture<TParameter, TParameterRepresentation, TRecord>.Sut => Sut;

        IArgumentExistenceRecorderMappingRepository<TParameter, TParameterRepresentation, TRecord> IFixture<TParameter, TParameterRepresentation, TRecord>.Repository => Repository;

        Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> IFixture<TParameter, TParameterRepresentation, TRecord>.ParameterRepresentationFactoryMock => ParameterRepresentationFactoryMock;
        Mock<IEqualityComparer<TParameterRepresentation>> IFixture<TParameter, TParameterRepresentation, TRecord>.ParameterRepresentationComparerMock => ParameterRepresentationComparerMock;
    }
}
