namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.T2;

using Moq;

using System;
using System.Collections.Generic;

using Xunit;

public sealed class Create
{
    private static IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TData> Target<TParameter, TParameterRepresentation, TRecord, TData>(IParameterMappingRepositoryFactory<TParameter, TParameterRepresentation> factory, IEqualityComparer<TParameterRepresentation> parameterRepresentationComparer) => factory.Create<TRecord, TData>(parameterRepresentationComparer);

    [Fact]
    public void NullParameterRepresentationComparer_ThrowsArgumentNullException()
    {
        var context = FactoryContext<object, object>.Create();

        var exception = Record.Exception(() => Target<object, object, object, object>(context.Factory, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void ValidParameterRepresentationComparer_ReturnsNotNull()
    {
        var context = FactoryContext<object, object>.Create();

        var actual = Target<object, object, object, object>(context.Factory, Mock.Of<IEqualityComparer<object>>());

        Assert.NotNull(actual);
    }
}
