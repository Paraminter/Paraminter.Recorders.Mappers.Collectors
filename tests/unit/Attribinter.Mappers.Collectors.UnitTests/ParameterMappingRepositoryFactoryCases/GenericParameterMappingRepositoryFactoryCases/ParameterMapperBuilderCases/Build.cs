namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.GenericParameterMappingRepositoryFactoryCases.ParameterMapperBuilderCases;

using System;

using Xunit;

public sealed class Build
{
    private static IParameterMapper<TParameter, TRecord, TData> Target<TParameter, TRecord, TData>(IParameterMapperBuilder<TParameter, TRecord, TData> builder) => builder.Build();

    [Fact]
    public void FirstBuild_ReturnsNotNull()
    {
        var context = BuilderContext<object, object, object>.Create();

        var actual = Target(context.Builder);

        Assert.NotNull(actual);
    }

    [Fact]
    public void SecondBuild_ThrowsInvalidOperationException()
    {
        var context = BuilderContext<object, object, object>.Create();

        context.Builder.Build();

        var exception = Record.Exception(() => Target(context.Builder));

        Assert.IsType<InvalidOperationException>(exception);
    }
}
