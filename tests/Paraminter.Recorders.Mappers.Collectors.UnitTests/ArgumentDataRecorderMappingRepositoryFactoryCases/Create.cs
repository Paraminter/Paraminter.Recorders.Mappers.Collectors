namespace Paraminter.Recorders.Mappers.Collectors.ArgumentDataRecorderMappingRepositoryFactoryCases;

using Moq;

using Paraminter.Parameters.Representations;

using System;
using System.Collections.Generic;

using Xunit;

public sealed class Create
{
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

    private IArgumentDataRecorderMappingRepository<TParameter, TParameterRepresentation, TRecord, TArgumentData> Target<TParameter, TParameterRepresentation, TRecord, TArgumentData>(
        IParameterRepresentationFactory<TParameter, TParameterRepresentation> parameterRepresentationFactory,
        IEqualityComparer<TParameterRepresentation> parameterRepresentationComparer)
    {
        return Fixture.Sut.Create<TParameter, TParameterRepresentation, TRecord, TArgumentData>(parameterRepresentationFactory, parameterRepresentationComparer);
    }
}
