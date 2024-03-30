namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.T2.ParameterMapperCases;

using Attribinter.Parameters.Representations;

using Moq;

using System;

using Xunit;

public sealed class TryMapParameter
{
    private static IMappedArgumentRecorder<TRecord, TData>? Target<TRecord, TData, TParameter>(IParameterMapper<TParameter, TRecord, TData> mapper, TParameter parameter) => mapper.TryMapParameter(parameter);

    [Fact]
    public void NullParameter_ThrowsArgumentNullException()
    {
        var context = MapperContext<object, object, object, object>.Create((_) => { }, (_) => { });

        var exception = Record.Exception(() => Target(context.Mapper, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void NonExisting_ReturnsNull()
    {
        var mappedParameterRepresentation = Mock.Of<object>();

        var parameter = Mock.Of<object>();
        var parameterRepresentation = Mock.Of<object>();

        var context = MapperContext<object, object, object, object>.Create(setupParameterComparer, registrator);

        context.ParameterRepresentationFactoryMock.Setup(static (factory) => factory.Create(It.IsAny<object>())).Returns(parameterRepresentation);

        var actual = Target(context.Mapper, parameter);

        Assert.Null(actual);

        context.ParameterRepresentationFactoryMock.Verify((factory) => factory.Create(parameter), Times.Once());
        context.ParameterRepresentationFactoryMock.VerifyNoOtherCalls();

        context.ParameterRepresentationComparerMock.Verify((comparer) => comparer.GetHashCode(mappedParameterRepresentation), Times.Once());
        context.ParameterRepresentationComparerMock.Verify((comparer) => comparer.GetHashCode(parameterRepresentation), Times.Once());
        context.ParameterRepresentationComparerMock.Verify((comparer) => comparer.Equals(mappedParameterRepresentation, parameterRepresentation), Times.Once());
        context.ParameterRepresentationComparerMock.VerifyNoOtherCalls();

        static void setupParameterComparer(Mock<IParameterRepresentationEqualityComparer<object>> parameterComparer) => parameterComparer.Setup(static (comparer) => comparer.GetHashCode(It.IsAny<object>())).Returns(42);

        void registrator(IParameterMappingCollector<object, object, object> collector) => collector.AddMapping(mappedParameterRepresentation, Mock.Of<IMappedArgumentRecorder<object, object>>());
    }

    [Fact]
    public void Existing_ReturnsRecorder()
    {
        var mappedParameterRepresentation = Mock.Of<object>();

        var parameter = Mock.Of<object>();
        var parameterRepresentation = Mock.Of<object>();

        var recorder = Mock.Of<IMappedArgumentRecorder<object, object>>();

        var context = MapperContext<object, object, object, object>.Create(setupParameterComparer, registrator);

        context.ParameterRepresentationFactoryMock.Setup(static (factory) => factory.Create(It.IsAny<object>())).Returns(parameterRepresentation);

        context.ParameterRepresentationComparerMock.Setup(static (comparer) => comparer.Equals(It.IsAny<object>(), It.IsAny<object>())).Returns(true);

        var actual = Target(context.Mapper, parameter);

        Assert.Same(recorder, actual);

        context.ParameterRepresentationFactoryMock.Verify((factory) => factory.Create(parameter), Times.Once());
        context.ParameterRepresentationFactoryMock.VerifyNoOtherCalls();

        context.ParameterRepresentationComparerMock.Verify((comparer) => comparer.GetHashCode(mappedParameterRepresentation), Times.Once());
        context.ParameterRepresentationComparerMock.Verify((comparer) => comparer.GetHashCode(parameterRepresentation), Times.Once());
        context.ParameterRepresentationComparerMock.Verify((comparer) => comparer.Equals(mappedParameterRepresentation, parameterRepresentation), Times.Once());
        context.ParameterRepresentationComparerMock.VerifyNoOtherCalls();

        static void setupParameterComparer(Mock<IParameterRepresentationEqualityComparer<object>> comparer) => comparer.Setup(static (comparer) => comparer.GetHashCode(It.IsAny<object>())).Returns(42);

        void registrator(IParameterMappingCollector<object, object, object> collector) => collector.AddMapping(mappedParameterRepresentation, recorder);
    }
}
