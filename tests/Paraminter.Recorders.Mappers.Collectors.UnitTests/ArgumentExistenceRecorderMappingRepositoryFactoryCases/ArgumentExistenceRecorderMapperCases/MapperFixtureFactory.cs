namespace Paraminter.Recorders.Mappers.Collectors.ArgumentExistenceRecorderMappingRepositoryFactoryCases.ArgumentExistenceRecorderMapperCases;

using Moq;

using Paraminter.Parameters.Representations;

using System;
using System.Collections.Generic;

internal static class MapperFixtureFactory
{
    public static IMapperFixture<TParameter, TParameterRepresentation, TRecord> Create<TParameter, TParameterRepresentation, TRecord>(
        Action<Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>>> parameterRepresentationFactoryMockSetup,
        Action<Mock<IEqualityComparer<TParameterRepresentation>>> parameterRepresentationComparerMockSetup,
        Action<IArgumentExistenceRecorderMappingCollector<TParameterRepresentation, TRecord>> registrator)
    {
        IArgumentExistenceRecorderMappingRepositoryFactory factory = new ArgumentExistenceRecorderMappingRepositoryFactory();

        Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> parameterRepresentationFactoryMock = new() { DefaultValue = DefaultValue.Mock };
        Mock<IEqualityComparer<TParameterRepresentation>> parameterRepresentationComparerMock = new() { DefaultValue = DefaultValue.Mock };

        parameterRepresentationFactoryMockSetup(parameterRepresentationFactoryMock);
        parameterRepresentationComparerMockSetup(parameterRepresentationComparerMock);

        var repository = factory.Create<TParameter, TParameterRepresentation, TRecord>(parameterRepresentationFactoryMock.Object, parameterRepresentationComparerMock.Object);

        registrator(repository.Collector);

        var sut = repository.Builder.Build();

        return new MapperFixture<TParameter, TParameterRepresentation, TRecord>(sut, parameterRepresentationFactoryMock, parameterRepresentationComparerMock);
    }

    private sealed class MapperFixture<TParameter, TParameterRepresentation, TRecord>
        : IMapperFixture<TParameter, TParameterRepresentation, TRecord>
    {
        private readonly IArgumentExistenceRecorderMapper<TParameter, TRecord> Sut;

        private readonly Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> ParameterRepresentationFactoryMock;
        private readonly Mock<IEqualityComparer<TParameterRepresentation>> ParameterRepresentationComparerMock;

        public MapperFixture(
            IArgumentExistenceRecorderMapper<TParameter, TRecord> sut,
            Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> parameterRepresentationFactoryMock,
            Mock<IEqualityComparer<TParameterRepresentation>> parameterRepresentationComparerMock)
        {
            Sut = sut;

            ParameterRepresentationFactoryMock = parameterRepresentationFactoryMock;
            ParameterRepresentationComparerMock = parameterRepresentationComparerMock;
        }

        IArgumentExistenceRecorderMapper<TParameter, TRecord> IMapperFixture<TParameter, TParameterRepresentation, TRecord>.Sut => Sut;

        Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> IMapperFixture<TParameter, TParameterRepresentation, TRecord>.ParameterRepresentationFactoryMock => ParameterRepresentationFactoryMock;
        Mock<IEqualityComparer<TParameterRepresentation>> IMapperFixture<TParameter, TParameterRepresentation, TRecord>.ParameterRepresentationComparerMock => ParameterRepresentationComparerMock;
    }
}
