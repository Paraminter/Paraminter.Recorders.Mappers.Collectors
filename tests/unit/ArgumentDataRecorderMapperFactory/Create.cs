namespace Paraminter.Recorders.Mappers.Collectors;

using Moq;

using System;

using Xunit;

public sealed class Create
{
    private readonly IFixture Fixture = FixtureFactory.Create();

    [Fact]
    public void NullRepository_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target<object, object, object, object>(null!, Mock.Of<IArgumentDataRecorderMappingRegistrator<object, object, object>>()));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void NullRegistrator_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target(Mock.Of<IArgumentDataRecorderMappingRepository<object, object, object, object>>(), null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void ValidArguments_ReturnsMapper()
    {
        var mapper = Mock.Of<IArgumentDataRecorderMapper<object, object, object>>();
        Mock<IArgumentDataRecorderMappingRepository<object, object, object, object>> repositoryMock = new() { DefaultValue = DefaultValue.Mock };

        repositoryMock.Setup(static (repository) => repository.Builder.Build()).Returns(mapper);

        var result = Target(repositoryMock.Object, Mock.Of<IArgumentDataRecorderMappingRegistrator<object, object, object>>());

        Assert.Same(mapper, result);
    }

    private IArgumentDataRecorderMapper<TParameter, TRecord, TArgumentData> Target<TParameter, TParameterRepresentation, TRecord, TArgumentData>(
        IArgumentDataRecorderMappingRepository<TParameter, TParameterRepresentation, TRecord, TArgumentData> mappingRepository,
        IArgumentDataRecorderMappingRegistrator<TParameterRepresentation, TRecord, TArgumentData> mappingRegistrator)
    {
        return Fixture.Sut.Create(mappingRepository, mappingRegistrator);
    }
}
