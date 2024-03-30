namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.T0;

using Attribinter.Parameters.Representations;

using Moq;

using System;

using Xunit;

public sealed class WithParameterRepresentation
{
    private static IParameterMappingRepositoryFactory<TParameter, TParameterRepresentation> Target<TParameter, TParameterRepresentation>(IParameterRepresentationFactory<TParameter, TParameterRepresentation> parameterRepresentationFactory) => Context.Factory.WithParameterRepresentation(parameterRepresentationFactory);

    private static readonly FactoryContext Context = FactoryContext.Create();

    [Fact]
    public void NullParameterRepresentationFactory_ThrowsArgumentNullException()
    {
        var exception = Record.Exception(() => Target<object, object>(null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void ValidParameterRepresentationFactory_ReturnsNotNull()
    {
        var actual = Target(Mock.Of<IParameterRepresentationFactory<object, object>>());

        Assert.NotNull(actual);
    }
}
