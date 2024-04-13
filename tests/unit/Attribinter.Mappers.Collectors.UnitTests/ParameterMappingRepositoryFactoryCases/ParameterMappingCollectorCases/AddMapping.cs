namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.ParameterMappingCollectorCases;

using Moq;

using System;

using Xunit;

public sealed class AddMapping
{
    private static void Target<TParameter, TParameterRepresentation, TRecord, TData>(ICollectorFixture<TParameter, TParameterRepresentation, TRecord, TData> fixture, TParameterRepresentation parameter, IMappedArgumentRecorder<TRecord, TData> recorder) => fixture.Sut.AddMapping(parameter, recorder);

    [Fact]
    public void NullParameter_ThrowsArgumentNullException()
    {
        var fixture = CollectorFixtureFactory.Create<object, object, object, object>();

        var result = Record.Exception(() => Target(fixture, null!, Mock.Of<IMappedArgumentRecorder<object, object>>()));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void NullRecorder_ThrowsArgumentNullException()
    {
        var fixture = CollectorFixtureFactory.Create<object, object, object, object>();

        var result = Record.Exception(() => Target(fixture, Mock.Of<object>(), null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void AlreadyExisting_ThrowsArgumentException()
    {
        var fixture = CollectorFixtureFactory.Create<object, object, object, object>();

        var parameter1 = Mock.Of<object>();
        var parameter2 = Mock.Of<object>();

        fixture.ParameterRepresentationComparerMock.Setup(static (comparer) => comparer.Equals(It.IsAny<object>(), It.IsAny<object>())).Returns(true);
        fixture.ParameterRepresentationComparerMock.Setup(static (comparer) => comparer.GetHashCode(It.IsAny<object>())).Returns(42);

        fixture.Sut.AddMapping(parameter1, Mock.Of<IMappedArgumentRecorder<object, object>>());

        var result = Record.Exception(() => Target(fixture, parameter2, Mock.Of<IMappedArgumentRecorder<object, object>>()));

        Assert.IsType<ArgumentException>(result);
    }

    [Fact]
    public void AlreadyBuilt_ThrowsInvalidOperationException()
    {
        var fixture = CollectorFixtureFactory.Create<object, object, object, object>();

        fixture.Repository.Builder.Build();

        var result = Record.Exception(() => Target(fixture, Mock.Of<object>(), Mock.Of<IMappedArgumentRecorder<object, object>>()));

        Assert.IsType<InvalidOperationException>(result);
    }
}
