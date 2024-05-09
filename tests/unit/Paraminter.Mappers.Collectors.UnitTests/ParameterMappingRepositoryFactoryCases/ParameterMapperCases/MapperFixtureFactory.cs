namespace Paraminter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.ParameterMapperCases;

using Moq;

using Paraminter.Parameters.Representations;

using System;
using System.Collections.Generic;

internal static class MapperFixtureFactory
{
    public static IMapperFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData> Create<TParameter, TParameterRepresentation, TRecord, TArgumentData>(Action<Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>>> parameterRepresentationFactoryMockSetup, Action<Mock<IEqualityComparer<TParameterRepresentation>>> parameterRepresentationComparerMockSetup, Action<IParameterMappingCollector<TParameterRepresentation, TRecord, TArgumentData>> registrator)
    {
        IParameterMappingRepositoryFactory factory = new ParameterMappingRepositoryFactory();

        Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> parameterRepresentationFactoryMock = new() { DefaultValue = DefaultValue.Mock };
        Mock<IEqualityComparer<TParameterRepresentation>> parameterRepresentationComparerMock = new();

        parameterRepresentationFactoryMockSetup(parameterRepresentationFactoryMock);
        parameterRepresentationComparerMockSetup(parameterRepresentationComparerMock);

        var repository = factory.Create<TParameter, TParameterRepresentation, TRecord, TArgumentData>(parameterRepresentationFactoryMock.Object, parameterRepresentationComparerMock.Object);

        registrator(repository.Collector);

        var sut = repository.Builder.Build();

        return new MapperFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>(sut, parameterRepresentationFactoryMock, parameterRepresentationComparerMock);
    }

    private sealed class MapperFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData> : IMapperFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>
    {
        private readonly IParameterMapper<TParameter, TRecord, TArgumentData> Sut;

        private readonly Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> ParameterRepresentationFactoryMock;
        private readonly Mock<IEqualityComparer<TParameterRepresentation>> ParameterRepresentationComparerMock;

        public MapperFixture(IParameterMapper<TParameter, TRecord, TArgumentData> sut, Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> parameterRepresentationFactoryMock, Mock<IEqualityComparer<TParameterRepresentation>> parameterRepresentationComparerMock)
        {
            Sut = sut;

            ParameterRepresentationFactoryMock = parameterRepresentationFactoryMock;
            ParameterRepresentationComparerMock = parameterRepresentationComparerMock;
        }

        IParameterMapper<TParameter, TRecord, TArgumentData> IMapperFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>.Sut => Sut;

        Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> IMapperFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>.ParameterRepresentationFactoryMock => ParameterRepresentationFactoryMock;
        Mock<IEqualityComparer<TParameterRepresentation>> IMapperFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>.ParameterRepresentationComparerMock => ParameterRepresentationComparerMock;
    }
}
