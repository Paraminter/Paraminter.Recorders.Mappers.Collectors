namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.ParameterMappingCollectorCases;

using Attribinter.Parameters.Representations;

using Moq;

using System.Collections.Generic;

internal static class CollectorFixtureFactory
{
    public static ICollectorFixture<TParameter, TParameterRepresentation, TRecord, TData> Create<TParameter, TParameterRepresentation, TRecord, TData>()
    {
        IParameterMappingRepositoryFactory factory = new ParameterMappingRepositoryFactory();

        Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> parameterRepresentationFactoryMock = new() { DefaultValue = DefaultValue.Mock };
        Mock<IEqualityComparer<TParameterRepresentation>> parameterRepresentationComparerMock = new();

        var repository = factory.Create<TParameter, TParameterRepresentation, TRecord, TData>(parameterRepresentationFactoryMock.Object, parameterRepresentationComparerMock.Object);

        return new CollectorFixture<TParameter, TParameterRepresentation, TRecord, TData>(repository.Collector, repository, parameterRepresentationFactoryMock, parameterRepresentationComparerMock);
    }

    private sealed class CollectorFixture<TParameter, TParameterRepresentation, TRecord, TData> : ICollectorFixture<TParameter, TParameterRepresentation, TRecord, TData>
    {
        private readonly IParameterMappingCollector<TParameterRepresentation, TRecord, TData> Sut;

        private readonly IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TData> Repository;

        private readonly Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> ParameterRepresentationFactoryMock;
        private readonly Mock<IEqualityComparer<TParameterRepresentation>> ParameterRepresentationComparerMock;

        public CollectorFixture(IParameterMappingCollector<TParameterRepresentation, TRecord, TData> sut, IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TData> repository, Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> parameterRepresentationFactoryMock, Mock<IEqualityComparer<TParameterRepresentation>> parameterRepresentationComparerMock)
        {
            Sut = sut;

            Repository = repository;

            ParameterRepresentationFactoryMock = parameterRepresentationFactoryMock;
            ParameterRepresentationComparerMock = parameterRepresentationComparerMock;
        }

        IParameterMappingCollector<TParameterRepresentation, TRecord, TData> ICollectorFixture<TParameter, TParameterRepresentation, TRecord, TData>.Sut => Sut;

        IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TData> ICollectorFixture<TParameter, TParameterRepresentation, TRecord, TData>.Repository => Repository;

        Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> ICollectorFixture<TParameter, TParameterRepresentation, TRecord, TData>.ParameterRepresentationFactoryMock => ParameterRepresentationFactoryMock;
        Mock<IEqualityComparer<TParameterRepresentation>> ICollectorFixture<TParameter, TParameterRepresentation, TRecord, TData>.ParameterRepresentationComparerMock => ParameterRepresentationComparerMock;
    }
}
