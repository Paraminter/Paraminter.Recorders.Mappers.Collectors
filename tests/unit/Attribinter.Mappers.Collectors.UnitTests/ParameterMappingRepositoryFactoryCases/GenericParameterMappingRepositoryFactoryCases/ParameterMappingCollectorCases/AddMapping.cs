namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.GenericParameterMappingRepositoryFactoryCases.ParameterMappingCollectorCases;

using Moq;

using System;

using Xunit;

public sealed class AddMapping
{
    private static void Target<TParameter, TRecord, TData>(IParameterMappingCollector<TParameter, TRecord, TData> collector, TParameter parameter, IMappedArgumentRecorder<TRecord, TData> recorder) => collector.AddMapping(parameter, recorder);

    [Fact]
    public void NullParameter_ThrowsArgumentNullException()
    {
        var context = CollectorContext<object, object, object, object>.Create();

        var exception = Record.Exception(() => Target(context.Collector, null!, Mock.Of<IMappedArgumentRecorder<object, object>>()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void NullRecorder_ThrowsArgumentNullException()
    {
        var context = CollectorContext<object, object, object, object>.Create();

        var exception = Record.Exception(() => Target(context.Collector, Mock.Of<object>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void NotAlreadyExisting_ThrowsNoException()
    {
        var context = CollectorContext<object, object, object, object>.Create();

        var parameter = Mock.Of<object>();

        var exception = Record.Exception(() => Target(context.Collector, parameter, Mock.Of<IMappedArgumentRecorder<object, object>>()));

        Assert.Null(exception);

        context.ParameterComparerMock.Verify((comparer) => comparer.GetHashCode(parameter), Times.Once());

        context.ParameterComparerMock.VerifyNoOtherCalls();
    }

    [Fact]
    public void AlreadyExisting_ThrowsArgumentException()
    {
        var context = CollectorContext<object, object, object, object>.Create();

        var parameter1 = Mock.Of<object>();
        var parameter2 = Mock.Of<object>();

        context.ParameterComparerMock.Setup(static (comparer) => comparer.Equals(It.IsAny<object>(), It.IsAny<object>())).Returns(true);
        context.ParameterComparerMock.Setup(static (comparer) => comparer.GetHashCode(It.IsAny<object>())).Returns(42);

        context.Collector.AddMapping(parameter1, Mock.Of<IMappedArgumentRecorder<object, object>>());

        var exception = Record.Exception(() => Target(context.Collector, parameter2, Mock.Of<IMappedArgumentRecorder<object, object>>()));

        Assert.IsType<ArgumentException>(exception);

        context.ParameterComparerMock.Verify((comparer) => comparer.GetHashCode(parameter1), Times.Once());
        context.ParameterComparerMock.Verify((comparer) => comparer.GetHashCode(parameter2), Times.Once());
        context.ParameterComparerMock.Verify((comparer) => comparer.Equals(parameter1, parameter2), Times.Once());

        context.ParameterComparerMock.VerifyNoOtherCalls();
    }

    [Fact]
    public void AlreadyBuilt_ThrowsInvalidOperationException()
    {
        var context = CollectorContext<object, object, object, object>.Create();

        context.Repository.Builder.Build();

        var exception = Record.Exception(() => Target(context.Collector, Mock.Of<object>(), Mock.Of<IMappedArgumentRecorder<object, object>>()));

        Assert.IsType<InvalidOperationException>(exception);
    }
}
