namespace Paraminter.Recorders.Mappers.Collectors.ArgumentExistenceRecorderMappingRepositoryFactoryCases.ArgumentExistenceRecorderMapperBuilderCases;

using System;

using Xunit;

public sealed class Build
{
    [Fact]
    public void MultipleBuilds_ThrowsInvalidOperationException()
    {
        var fixture = BuilderFixtureFactory.Create<object, object, object>();

        fixture.Sut.Build();

        var result = Record.Exception(() => Target(fixture));

        Assert.IsType<InvalidOperationException>(result);
    }

    [Fact]
    public void FirstBuild_ReturnsMapper()
    {
        var fixture = BuilderFixtureFactory.Create<object, object, object>();

        var result = Target(fixture);

        Assert.NotNull(result);
    }

    private static IArgumentExistenceRecorderMapper<TParameter, TRecord> Target<TParameter, TParameterRepresentation, TRecord>(
        IBuilderFixture<TParameter, TParameterRepresentation, TRecord> fixture)
    {
        return fixture.Sut.Build();
    }
}
