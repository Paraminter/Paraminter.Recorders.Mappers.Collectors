namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.ParameterMapperCases;

using Attribinter.Parameters.Representations;

using Moq;

using System;
using System.Collections.Generic;

internal static class MapperFixtureFactory
{
    public static IMapperFixture<TParameter, TParameterRepresentation, TRecord, TData> Create<TParameter, TParameterRepresentation, TRecord, TData>(Action<Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>>> parameterRepresentationFactoryMockSetup, Action<Mock<IEqualityComparer<TParameterRepresentation>>> parameterRepresentationComparerMockSetup, Action<IParameterMappingCollector<TParameterRepresentation, TRecord, TData>> registrator)
    {
        IParameterMappingRepositoryFactory factory = new ParameterMappingRepositoryFactory();

        Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> parameterRepresentationFactoryMock = new() { DefaultValue = DefaultValue.Mock };
        Mock<IEqualityComparer<TParameterRepresentation>> parameterRepresentationComparerMock = new();

        parameterRepresentationFactoryMockSetup(parameterRepresentationFactoryMock);
        parameterRepresentationComparerMockSetup(parameterRepresentationComparerMock);

        var repository = factory.Create<TParameter, TParameterRepresentation, TRecord, TData>(parameterRepresentationFactoryMock.Object, parameterRepresentationComparerMock.Object);

        registrator(repository.Collector);

        var sut = repository.Builder.Build();

        return new MapperFixture<TParameter, TParameterRepresentation, TRecord, TData>(sut, parameterRepresentationFactoryMock, parameterRepresentationComparerMock);
    }

    private sealed class MapperFixture<TParameter, TParameterRepresentation, TRecord, TData> : IMapperFixture<TParameter, TParameterRepresentation, TRecord, TData>
    {
        private readonly IParameterMapper<TParameter, TRecord, TData> Sut;

        private readonly Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> ParameterRepresentationFactoryMock;
        private readonly Mock<IEqualityComparer<TParameterRepresentation>> ParameterRepresentationComparerMock;

        public MapperFixture(IParameterMapper<TParameter, TRecord, TData> sut, Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> parameterRepresentationFactoryMock, Mock<IEqualityComparer<TParameterRepresentation>> parameterRepresentationComparerMock)
        {
            Sut = sut;

            ParameterRepresentationFactoryMock = parameterRepresentationFactoryMock;
            ParameterRepresentationComparerMock = parameterRepresentationComparerMock;
        }

        IParameterMapper<TParameter, TRecord, TData> IMapperFixture<TParameter, TParameterRepresentation, TRecord, TData>.Sut => Sut;

        Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> IMapperFixture<TParameter, TParameterRepresentation, TRecord, TData>.ParameterRepresentationFactoryMock => ParameterRepresentationFactoryMock;
        Mock<IEqualityComparer<TParameterRepresentation>> IMapperFixture<TParameter, TParameterRepresentation, TRecord, TData>.ParameterRepresentationComparerMock => ParameterRepresentationComparerMock;
    }
}
