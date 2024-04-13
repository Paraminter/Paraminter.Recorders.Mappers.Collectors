namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.ParameterMapperBuilderCases;

using System;

using Xunit;

public sealed class Build
{
    private static IParameterMapper<TParameter, TRecord, TData> Target<TParameter, TParameterRepresentation, TRecord, TData>(IBuilderFixture<TParameter, TParameterRepresentation, TRecord, TData> fixture) => fixture.Sut.Build();

    [Fact]
    public void MultipleBuilds_ThrowsInvalidOperationException()
    {
        var fixture = BuilderFixtureFactory.Create<object, object, object, object>();

        fixture.Sut.Build();

        var result = Record.Exception(() => Target(fixture));

        Assert.IsType<InvalidOperationException>(result);
    }
}
