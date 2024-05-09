namespace Paraminter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.ParameterMappingCollectorCases;

using Moq;

using Paraminter.Parameters.Representations;

using System.Collections.Generic;

internal static class CollectorFixtureFactory
{
    public static ICollectorFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData> Create<TParameter, TParameterRepresentation, TRecord, TArgumentData>()
    {
        IParameterMappingRepositoryFactory factory = new ParameterMappingRepositoryFactory();

        Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> parameterRepresentationFactoryMock = new() { DefaultValue = DefaultValue.Mock };
        Mock<IEqualityComparer<TParameterRepresentation>> parameterRepresentationComparerMock = new();

        var repository = factory.Create<TParameter, TParameterRepresentation, TRecord, TArgumentData>(parameterRepresentationFactoryMock.Object, parameterRepresentationComparerMock.Object);

        return new CollectorFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>(repository.Collector, repository, parameterRepresentationFactoryMock, parameterRepresentationComparerMock);
    }

    private sealed class CollectorFixture<TParameter, TParameterRepresentation, TRecord, TDArgumentata> : ICollectorFixture<TParameter, TParameterRepresentation, TRecord, TDArgumentata>
    {
        private readonly IParameterMappingCollector<TParameterRepresentation, TRecord, TDArgumentata> Sut;

        private readonly IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TDArgumentata> Repository;

        private readonly Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> ParameterRepresentationFactoryMock;
        private readonly Mock<IEqualityComparer<TParameterRepresentation>> ParameterRepresentationComparerMock;

        public CollectorFixture(IParameterMappingCollector<TParameterRepresentation, TRecord, TDArgumentata> sut, IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TDArgumentata> repository, Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> parameterRepresentationFactoryMock, Mock<IEqualityComparer<TParameterRepresentation>> parameterRepresentationComparerMock)
        {
            Sut = sut;

            Repository = repository;

            ParameterRepresentationFactoryMock = parameterRepresentationFactoryMock;
            ParameterRepresentationComparerMock = parameterRepresentationComparerMock;
        }

        IParameterMappingCollector<TParameterRepresentation, TRecord, TDArgumentata> ICollectorFixture<TParameter, TParameterRepresentation, TRecord, TDArgumentata>.Sut => Sut;

        IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TDArgumentata> ICollectorFixture<TParameter, TParameterRepresentation, TRecord, TDArgumentata>.Repository => Repository;

        Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> ICollectorFixture<TParameter, TParameterRepresentation, TRecord, TDArgumentata>.ParameterRepresentationFactoryMock => ParameterRepresentationFactoryMock;
        Mock<IEqualityComparer<TParameterRepresentation>> ICollectorFixture<TParameter, TParameterRepresentation, TRecord, TDArgumentata>.ParameterRepresentationComparerMock => ParameterRepresentationComparerMock;
    }
}
