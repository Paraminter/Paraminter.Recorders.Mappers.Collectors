namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases;

using Moq;

using System;
using System.Collections.Generic;

using Xunit;

public sealed class ForParameter
{
    private IParameterMappingRepositoryFactory<TParameter, TParameterRepresentation> Target<TParameter, TParameterRepresentation>(IParameterRepresentationFactory<TParameter, TParameterRepresentation> parameterRepresentationFactory, IEqualityComparer<TParameterRepresentation> parameterComparer) => Target(Context.Factory, parameterRepresentationFactory, parameterComparer);
    private static IParameterMappingRepositoryFactory<TParameter, TParameterRepresentation> Target<TParameter, TParameterRepresentation>(IParameterMappingRepositoryFactory factory, IParameterRepresentationFactory<TParameter, TParameterRepresentation> parameterRepresentationFactory, IEqualityComparer<TParameterRepresentation> parameterComparer) => factory.ForParameter(parameterRepresentationFactory, parameterComparer);

    private readonly FactoryContext Context = FactoryContext.Create();

    [Fact]
    public void NullParameterRepresentationFactory_ThrowsArgumentNullException()
    {
        var exception = Record.Exception(() => Target<object, object>(null!, Mock.Of<IEqualityComparer<object>>()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void NullParameterComparer_ThrowsArgumentNullException()
    {
        var exception = Record.Exception(() => Target(Mock.Of<IParameterRepresentationFactory<object, object>>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void ValidArguments_ReturnsNotNull()
    {
        var actual = Target(Mock.Of<IParameterRepresentationFactory<object, object>>(), Mock.Of<IEqualityComparer<object>>());

        Assert.NotNull(actual);
    }
}
