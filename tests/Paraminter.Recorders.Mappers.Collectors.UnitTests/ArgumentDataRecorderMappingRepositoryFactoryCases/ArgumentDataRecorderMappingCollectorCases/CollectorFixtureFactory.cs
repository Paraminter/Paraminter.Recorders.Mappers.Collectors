namespace Paraminter.Recorders.Mappers.Collectors.ArgumentDataRecorderMappingRepositoryFactoryCases.ArgumentDataRecorderMappingCollectorCases;

using Moq;

using Paraminter.Parameters.Representations;

using System.Collections.Generic;

internal static class CollectorFixtureFactory
{
    public static ICollectorFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData> Create<TParameter, TParameterRepresentation, TRecord, TArgumentData>()
    {
        IArgumentDataRecorderMappingRepositoryFactory factory = new ArgumentDataRecorderMappingRepositoryFactory();

        Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> parameterRepresentationFactoryMock = new() { DefaultValue = DefaultValue.Mock };
        Mock<IEqualityComparer<TParameterRepresentation>> parameterRepresentationComparerMock = new() { DefaultValue = DefaultValue.Mock };

        var repository = factory.Create<TParameter, TParameterRepresentation, TRecord, TArgumentData>(parameterRepresentationFactoryMock.Object, parameterRepresentationComparerMock.Object);

        return new CollectorFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>(repository.Collector, repository, parameterRepresentationFactoryMock, parameterRepresentationComparerMock);
    }

    private sealed class CollectorFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>
        : ICollectorFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>
    {
        private readonly IArgumentDataRecorderMappingCollector<TParameterRepresentation, TRecord, TArgumentData> Sut;

        private readonly IArgumentDataRecorderMappingRepository<TParameter, TParameterRepresentation, TRecord, TArgumentData> Repository;

        private readonly Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> ParameterRepresentationFactoryMock;
        private readonly Mock<IEqualityComparer<TParameterRepresentation>> ParameterRepresentationComparerMock;

        public CollectorFixture(
            IArgumentDataRecorderMappingCollector<TParameterRepresentation, TRecord, TArgumentData> sut,
            IArgumentDataRecorderMappingRepository<TParameter, TParameterRepresentation, TRecord, TArgumentData> repository,
            Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> parameterRepresentationFactoryMock,
            Mock<IEqualityComparer<TParameterRepresentation>> parameterRepresentationComparerMock)
        {
            Sut = sut;

            Repository = repository;

            ParameterRepresentationFactoryMock = parameterRepresentationFactoryMock;
            ParameterRepresentationComparerMock = parameterRepresentationComparerMock;
        }

        IArgumentDataRecorderMappingCollector<TParameterRepresentation, TRecord, TArgumentData> ICollectorFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>.Sut => Sut;

        IArgumentDataRecorderMappingRepository<TParameter, TParameterRepresentation, TRecord, TArgumentData> ICollectorFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>.Repository => Repository;

        Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> ICollectorFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>.ParameterRepresentationFactoryMock => ParameterRepresentationFactoryMock;
        Mock<IEqualityComparer<TParameterRepresentation>> ICollectorFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>.ParameterRepresentationComparerMock => ParameterRepresentationComparerMock;
    }
}
