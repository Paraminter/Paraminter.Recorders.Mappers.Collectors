namespace Paraminter.Recorders.Mappers.Collectors.ArgumentExistenceRecorderMappingRepositoryFactoryCases.ArgumentExistenceRecorderMappingCollectorCases;

using Moq;

using System;

using Xunit;

public sealed class AddMapping
{
    [Fact]
    public void NullParameter_ThrowsArgumentNullException()
    {
        var fixture = CollectorFixtureFactory.Create<object, object, object>();

        var result = Record.Exception(() => Target(fixture, null!, Mock.Of<IMappedArgumentExistenceRecorder<object>>()));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void NullRecorder_ThrowsArgumentNullException()
    {
        var fixture = CollectorFixtureFactory.Create<object, object, object>();

        var result = Record.Exception(() => Target(fixture, Mock.Of<object>(), null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void AlreadyExisting_ThrowsArgumentException()
    {
        var fixture = CollectorFixtureFactory.Create<object, object, object>();

        var parameter1 = Mock.Of<object>();
        var parameter2 = Mock.Of<object>();

        fixture.ParameterRepresentationComparerMock.Setup((comparer) => comparer.Equals(parameter1, parameter2)).Returns(true);
        fixture.ParameterRepresentationComparerMock.Setup((comparer) => comparer.GetHashCode(parameter1)).Returns(42);
        fixture.ParameterRepresentationComparerMock.Setup((comparer) => comparer.GetHashCode(parameter2)).Returns(42);

        fixture.Sut.AddMapping(parameter1, Mock.Of<IMappedArgumentExistenceRecorder<object>>());

        var result = Record.Exception(() => Target(fixture, parameter2, Mock.Of<IMappedArgumentExistenceRecorder<object>>()));

        Assert.IsType<ArgumentException>(result);
    }

    [Fact]
    public void AlreadyBuilt_ThrowsInvalidOperationException()
    {
        var fixture = CollectorFixtureFactory.Create<object, object, object>();

        fixture.Repository.Builder.Build();

        var result = Record.Exception(() => Target(fixture, Mock.Of<object>(), Mock.Of<IMappedArgumentExistenceRecorder<object>>()));

        Assert.IsType<InvalidOperationException>(result);
    }

    [Fact]
    public void NotExistingAndFirstBuild_ThrowsNoException()
    {
        var fixture = CollectorFixtureFactory.Create<object, object, object>();

        var result = Record.Exception(() => Target(fixture, Mock.Of<object>(), Mock.Of<IMappedArgumentExistenceRecorder<object>>()));

        Assert.Null(result);
    }

    private static void Target<TParameter, TParameterRepresentation, TRecord>(
        ICollectorFixture<TParameter, TParameterRepresentation, TRecord> fixture,
        TParameterRepresentation parameter,
        IMappedArgumentExistenceRecorder<TRecord> recorder)
    {
        fixture.Sut.AddMapping(parameter, recorder);
    }
}
