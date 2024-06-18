namespace Paraminter.Recorders.Mappers.Collectors.ArgumentExistenceRecorderMapper;

using Moq;

using Paraminter.Parameters.Representations;

using System;
using System.Collections.Generic;

internal static class FixtureFactory
{
    public static IFixture<TParameter, TParameterRepresentation, TRecord> Create<TParameter, TParameterRepresentation, TRecord>(
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

        return new Fixture<TParameter, TParameterRepresentation, TRecord>(sut, parameterRepresentationFactoryMock, parameterRepresentationComparerMock);
    }

    private sealed class Fixture<TParameter, TParameterRepresentation, TRecord>
        : IFixture<TParameter, TParameterRepresentation, TRecord>
    {
        private readonly IArgumentExistenceRecorderMapper<TParameter, TRecord> Sut;

        private readonly Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> ParameterRepresentationFactoryMock;
        private readonly Mock<IEqualityComparer<TParameterRepresentation>> ParameterRepresentationComparerMock;

        public Fixture(
            IArgumentExistenceRecorderMapper<TParameter, TRecord> sut,
            Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> parameterRepresentationFactoryMock,
            Mock<IEqualityComparer<TParameterRepresentation>> parameterRepresentationComparerMock)
        {
            Sut = sut;

            ParameterRepresentationFactoryMock = parameterRepresentationFactoryMock;
            ParameterRepresentationComparerMock = parameterRepresentationComparerMock;
        }

        IArgumentExistenceRecorderMapper<TParameter, TRecord> IFixture<TParameter, TParameterRepresentation, TRecord>.Sut => Sut;

        Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> IFixture<TParameter, TParameterRepresentation, TRecord>.ParameterRepresentationFactoryMock => ParameterRepresentationFactoryMock;
        Mock<IEqualityComparer<TParameterRepresentation>> IFixture<TParameter, TParameterRepresentation, TRecord>.ParameterRepresentationComparerMock => ParameterRepresentationComparerMock;
    }
}
