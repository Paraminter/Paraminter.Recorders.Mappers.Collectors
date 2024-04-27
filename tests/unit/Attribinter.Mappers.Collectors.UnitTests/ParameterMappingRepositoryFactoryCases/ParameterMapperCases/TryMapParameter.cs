namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.ParameterMapperCases;

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

        static void setupParameterComparer(Mock<IEqualityComparer<object>> parameterComparer) => parameterComparer.Setup(static (comparer) => comparer.GetHashCode(It.IsAny<object>())).Returns(42);

        void registrator(IParameterMappingCollector<object, object, object> collector) => collector.AddMapping(mappedParameterRepresentation, Mock.Of<IMappedArgumentRecorder<object, object>>());
    }

    [Fact]
    public void Existing_ReturnsRecorder()
    {
        var mappedParameterRepresentation = Mock.Of<object>();

        var parameter = Mock.Of<object>();
        var parameterRepresentation = Mock.Of<object>();

        var recorder = Mock.Of<IMappedArgumentRecorder<object, object>>();

        var fixture = MapperFixtureFactory.Create<object, object, object, object>((_) => { }, setupParameterComparer, registrator);

        fixture.ParameterRepresentationFactoryMock.Setup((factory) => factory.Create(parameter)).Returns(parameterRepresentation);

        fixture.ParameterRepresentationComparerMock.Setup(static (comparer) => comparer.Equals(It.IsAny<object>(), It.IsAny<object>())).Returns(true);

        var result = Target(fixture, parameter);

        Assert.Same(recorder, result);

        static void setupParameterComparer(Mock<IEqualityComparer<object>> comparer) => comparer.Setup(static (comparer) => comparer.GetHashCode(It.IsAny<object>())).Returns(42);

        void registrator(IParameterMappingCollector<object, object, object> collector) => collector.AddMapping(mappedParameterRepresentation, recorder);
    }

    private static IMappedArgumentRecorder<TRecord, TData>? Target<TParameter, TParameterRepresentation, TRecord, TData>(IMapperFixture<TParameter, TParameterRepresentation, TRecord, TData> fixture, TParameter parameter) => fixture.Sut.TryMapParameter(parameter);
}
