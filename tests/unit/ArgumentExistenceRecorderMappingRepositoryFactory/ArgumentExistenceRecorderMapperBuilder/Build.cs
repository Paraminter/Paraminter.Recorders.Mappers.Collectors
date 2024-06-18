namespace Paraminter.Recorders.Mappers.Collectors.ArgumentExistenceRecorderMapperBuilder;

using System;

using Xunit;

public sealed class Build
{
    [Fact]
    public void MultipleBuilds_ThrowsInvalidOperationException()
    {
        var fixture = FixtureFactory.Create<object, object, object>();

        fixture.Sut.Build();

        var result = Record.Exception(() => Target(fixture));

        Assert.IsType<InvalidOperationException>(result);
    }

    [Fact]
    public void FirstBuild_ReturnsMapper()
    {
        var fixture = FixtureFactory.Create<object, object, object>();

        var result = Target(fixture);

        Assert.NotNull(result);
    }

    private static IArgumentExistenceRecorderMapper<TParameter, TRecord> Target<TParameter, TParameterRepresentation, TRecord>(
        IFixture<TParameter, TParameterRepresentation, TRecord> fixture)
    {
        return fixture.Sut.Build();
    }
}
