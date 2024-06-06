namespace Paraminter.Recorders.Mappers.Collectors.ArgumentDataRecorderMappingRepositoryFactoryCases.ArgumentDataRecorderMapperCases;

using Moq;

using System;
using System.Collections.Generic;

using Xunit;

public sealed class TryMapParameter
{
    [Fact]
    public void NullParameter_ThrowsArgumentNullException()
    {
        var fixture = MapperFixtureFactory.Create<object, object, object, object>((_) => { }, (_) => { }, (_) => { });

        var result = Record.Exception(() => Target(fixture, null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void NonExisting_ReturnsNull()
    {
        var mappedParameterRepresentation = Mock.Of<object>();

        var parameter = Mock.Of<object>();
        var parameterRepresentation = Mock.Of<object>();

        var fixture = MapperFixtureFactory.Create<object, object, object, object>((_) => { }, setupParameterComparer, registrator);

        fixture.ParameterRepresentationFactoryMock.Setup((factory) => factory.Create(parameter)).Returns(parameterRepresentation);

        var result = Target(fixture, parameter);

        Assert.Null(result);

        void setupParameterComparer(
            Mock<IEqualityComparer<object>> parameterComparer)
        {
            parameterComparer.Setup((comparer) => comparer.GetHashCode(mappedParameterRepresentation)).Returns(42);
            parameterComparer.Setup((comparer) => comparer.GetHashCode(parameterRepresentation)).Returns(42);
        }

        void registrator(
            IArgumentDataRecorderMappingCollector<object, object, object> collector)
        {
            collector.AddMapping(mappedParameterRepresentation, Mock.Of<IMappedArgumentDataRecorder<object, object>>());
        }
    }

    [Fact]
    public void Existing_ReturnsRecorder()
    {
        var mappedParameterRepresentation = Mock.Of<object>();

        var parameter = Mock.Of<object>();
        var parameterRepresentation = Mock.Of<object>();

        var recorder = Mock.Of<IMappedArgumentDataRecorder<object, object>>();

        var fixture = MapperFixtureFactory.Create<object, object, object, object>((_) => { }, setupParameterComparer, registrator);

        fixture.ParameterRepresentationFactoryMock.Setup((factory) => factory.Create(parameter)).Returns(parameterRepresentation);

        fixture.ParameterRepresentationComparerMock.Setup((comparer) => comparer.Equals(mappedParameterRepresentation, parameterRepresentation)).Returns(true);

        var result = Target(fixture, parameter);

        Assert.Same(recorder, result);

        void setupParameterComparer(
            Mock<IEqualityComparer<object>> parameterComparer)
        {
            parameterComparer.Setup((comparer) => comparer.GetHashCode(mappedParameterRepresentation)).Returns(42);
            parameterComparer.Setup((comparer) => comparer.GetHashCode(parameterRepresentation)).Returns(42);
        }

        void registrator(
            IArgumentDataRecorderMappingCollector<object, object, object> collector)
        {
            collector.AddMapping(mappedParameterRepresentation, recorder);
        }
    }

    private static IMappedArgumentDataRecorder<TRecord, TArgumentData>? Target<TParameter, TParameterRepresentation, TRecord, TArgumentData>(
        IMapperFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData> fixture,
        TParameter parameter)
    {
        return fixture.Sut.TryMapParameter(parameter);
    }
}
