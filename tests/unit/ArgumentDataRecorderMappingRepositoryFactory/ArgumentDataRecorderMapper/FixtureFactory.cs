namespace Paraminter.Recorders.Mappers.Collectors.ArgumentDataRecorderMapper;

using Moq;

using Paraminter.Parameters.Representations;

using System;
using System.Collections.Generic;

internal static class FixtureFactory
{
    public static IFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData> Create<TParameter, TParameterRepresentation, TRecord, TArgumentData>(
        Action<Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>>> parameterRepresentationFactoryMockSetup,
        Action<Mock<IEqualityComparer<TParameterRepresentation>>> parameterRepresentationComparerMockSetup,
        Action<IArgumentDataRecorderMappingCollector<TParameterRepresentation, TRecord, TArgumentData>> registrator)
    {
        IArgumentDataRecorderMappingRepositoryFactory factory = new ArgumentDataRecorderMappingRepositoryFactory();

        Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> parameterRepresentationFactoryMock = new() { DefaultValue = DefaultValue.Mock };
        Mock<IEqualityComparer<TParameterRepresentation>> parameterRepresentationComparerMock = new() { DefaultValue = DefaultValue.Mock };

        parameterRepresentationFactoryMockSetup(parameterRepresentationFactoryMock);
        parameterRepresentationComparerMockSetup(parameterRepresentationComparerMock);

        var repository = factory.Create<TParameter, TParameterRepresentation, TRecord, TArgumentData>(parameterRepresentationFactoryMock.Object, parameterRepresentationComparerMock.Object);

        registrator(repository.Collector);

        var sut = repository.Builder.Build();

        return new Fixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>(sut, parameterRepresentationFactoryMock, parameterRepresentationComparerMock);
    }

    private sealed class Fixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>
        : IFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>
    {
        private readonly IArgumentDataRecorderMapper<TParameter, TRecord, TArgumentData> Sut;

        private readonly Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> ParameterRepresentationFactoryMock;
        private readonly Mock<IEqualityComparer<TParameterRepresentation>> ParameterRepresentationComparerMock;

        public Fixture(
            IArgumentDataRecorderMapper<TParameter, TRecord, TArgumentData> sut,
            Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> parameterRepresentationFactoryMock,
            Mock<IEqualityComparer<TParameterRepresentation>> parameterRepresentationComparerMock)
        {
            Sut = sut;

            ParameterRepresentationFactoryMock = parameterRepresentationFactoryMock;
            ParameterRepresentationComparerMock = parameterRepresentationComparerMock;
        }

        IArgumentDataRecorderMapper<TParameter, TRecord, TArgumentData> IFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>.Sut => Sut;

        Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> IFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>.ParameterRepresentationFactoryMock => ParameterRepresentationFactoryMock;
        Mock<IEqualityComparer<TParameterRepresentation>> IFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData>.ParameterRepresentationComparerMock => ParameterRepresentationComparerMock;
    }
}
