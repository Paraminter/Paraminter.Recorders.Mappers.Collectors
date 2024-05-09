namespace Paraminter.Mappers.Collectors.ParameterMapperFactoryCases;

using Moq;

using System;

using Xunit;

public sealed class Create
{
    private IParameterMapper<TParameter, TRecord, TArgumentData> Target<TParameter, TParameterRepresentation, TRecord, TArgumentData>(IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TArgumentData> parameterMappingRepository, IParameterMappingRegistrator<TParameterRepresentation, TRecord, TArgumentData> parameterMappingRegistrator) => Fixture.Sut.Create(parameterMappingRepository, parameterMappingRegistrator);

    private readonly IFactoryFixture Fixture = FactoryFixtureFactory.Create();

    [Fact]
    public void NullRepository_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target<object, object, object, object>(null!, Mock.Of<IParameterMappingRegistrator<object, object, object>>()));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void NullRegistrator_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target(Mock.Of<IParameterMappingRepository<object, object, object, object>>(), null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void ValidArguments_ReturnsMapper()
    {
        var mapper = Mock.Of<IParameterMapper<object, object, object>>();
        Mock<IParameterMappingRepository<object, object, object, object>> repositoryMock = new();

        repositoryMock.Setup(static (repository) => repository.Builder.Build()).Returns(mapper);

        var result = Target(repositoryMock.Object, Mock.Of<IParameterMappingRegistrator<object, object, object>>());

        Assert.Same(mapper, result);
    }
}
