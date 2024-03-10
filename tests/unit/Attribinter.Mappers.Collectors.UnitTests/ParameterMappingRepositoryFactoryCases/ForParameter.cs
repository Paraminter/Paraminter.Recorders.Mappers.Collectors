namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases;

using Moq;

using System;
using System.Collections.Generic;

using Xunit;

public sealed class ForParameter
{
    private IParameterMappingRepositoryFactory<TParameter> Target<TParameter>(IEqualityComparer<TParameter> parameterComparer) => Target(Context.Factory, parameterComparer);
    private static IParameterMappingRepositoryFactory<TParameter> Target<TParameter>(IParameterMappingRepositoryFactory factory, IEqualityComparer<TParameter> parameterComparer) => factory.ForParameter(parameterComparer);

    private readonly FactoryContext Context = FactoryContext.Create();

    [Fact]
    public void NullParameterComparer_ThrowsArgumentNullException()
    {
        var exception = Record.Exception(() => Target<object>(null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void ValidParameteComparer_ReturnsNotNull()
    {
        var actual = Target(Mock.Of<IEqualityComparer<object>>());

        Assert.NotNull(actual);
    }
}
