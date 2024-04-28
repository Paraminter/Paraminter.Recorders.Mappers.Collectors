namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.T2;

using Attribinter.Parameters.Representations;

using Moq;

using System;
using System.Collections.Generic;

using Xunit;

public sealed class Create
{
    private IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TData> Target<TParameter, TParameterRepresentation, TRecord, TData>(IParameterRepresentationFactory<TParameter, TParameterRepresentation> parameterRepresentationFactory, IEqualityComparer<TParameterRepresentation> parameterRepresentationComparer) => Fixture.Sut.Create<TParameter, TParameterRepresentation, TRecord, TData>(parameterRepresentationFactory, parameterRepresentationComparer);

    private readonly IFactoryFixture Fixture = FactoryFixtureFactory.Create();

    [Fact]
    public void NullParameterRepresentationFactory_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target<object, object, object, object>(null!, Mock.Of<IEqualityComparer<object>>()));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void NullParameterRepresentationComparer_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target<object, object, object, object>(Mock.Of<IParameterRepresentationFactory<object, object>>(), null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void ValidArguments_ReturnsRepository()
    {
        var result = Target<object, object, object, object>(Mock.Of<IParameterRepresentationFactory<object, object>>(), Mock.Of<IEqualityComparer<object>>());

        Assert.NotNull(result);
    }
}
