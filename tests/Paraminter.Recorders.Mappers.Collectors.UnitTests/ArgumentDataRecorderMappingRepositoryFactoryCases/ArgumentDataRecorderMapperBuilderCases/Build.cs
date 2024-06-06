namespace Paraminter.Recorders.Mappers.Collectors.ArgumentDataRecorderMappingRepositoryFactoryCases.ArgumentDataRecorderMapperBuilderCases;

using System;

using Xunit;

public sealed class Build
{
    [Fact]
    public void MultipleBuilds_ThrowsInvalidOperationException()
    {
        var fixture = BuilderFixtureFactory.Create<object, object, object, object>();

        fixture.Sut.Build();

        var result = Record.Exception(() => Target(fixture));

        Assert.IsType<InvalidOperationException>(result);
    }

    [Fact]
    public void FirstBuild_ReturnsMapper()
    {
        var fixture = BuilderFixtureFactory.Create<object, object, object, object>();

        var result = Target(fixture);

        Assert.NotNull(result);
    }

    private static IArgumentDataRecorderMapper<TParameter, TRecord, TArgumentData> Target<TParameter, TParameterRepresentation, TRecord, TArgumentData>(
        IBuilderFixture<TParameter, TParameterRepresentation, TRecord, TArgumentData> fixture)
    {
        return fixture.Sut.Build();
    }
}
