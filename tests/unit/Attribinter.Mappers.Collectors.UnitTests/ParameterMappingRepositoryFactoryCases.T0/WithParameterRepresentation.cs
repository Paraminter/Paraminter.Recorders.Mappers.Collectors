namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.T0;

using Moq;

using System;

using Xunit;

public sealed class WithParameterRepresentation
{
    private static IParameterMappingRepositoryFactory<TParameter, TParameterRepresentation> Target<TParameter, TParameterRepresentation>(IParameterRepresentationFactory<TParameter, TParameterRepresentation> parameterRepresentationFactory, IParameterRepresentationEqualityComparer<TParameterRepresentation> parameterRepresentationComparer) => Context.Factory.WithParameterRepresentation(parameterRepresentationFactory, parameterRepresentationComparer);

    private static readonly FactoryContext Context = FactoryContext.Create();

    [Fact]
    public void NullParameterRepresentationFactory_ThrowsArgumentNullException()
    {
        var exception = Record.Exception(() => Target<object, object>(null!, Mock.Of<IParameterRepresentationEqualityComparer<object>>()));

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
        var actual = Target(Mock.Of<IParameterRepresentationFactory<object, object>>(), Mock.Of<IParameterRepresentationEqualityComparer<object>>());

        Assert.NotNull(actual);
    }
}
