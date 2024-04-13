namespace Attribinter.Mappers.Collectors.ParameterMapperFactoryCases;

using Moq;

using System;

using Xunit;

public sealed class Create
{
    private IParameterMapper<TParameter, TRecord, TData> Target<TParameter, TParameterRepresentation, TRecord, TData>(IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TData> parameterMappingRepository, IParameterMappingRegistrator<TParameterRepresentation, TRecord, TData> parameterMappingRegistrator) => Fixture.Sut.Create(parameterMappingRepository, parameterMappingRegistrator);

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
        var collector = Mock.Of<IParameterMappingCollector<object, object, object>>();
        Mock<IParameterMappingRepository<object, object, object, object>> repositoryMock = new() { DefaultValue = DefaultValue.Mock };
        Mock<IParameterMappingRegistrator<object, object, object>> registratorMock = new();

        repositoryMock.Setup(static (repository) => repository.Builder.Build()).Returns(mapper);
        repositoryMock.Setup(static (repository) => repository.Collector).Returns(collector);

        var result = Target(repositoryMock.Object, registratorMock.Object);

        Assert.Same(mapper, result);
    }
}
