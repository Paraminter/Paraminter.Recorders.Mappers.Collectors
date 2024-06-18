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
        var result = Record.Exception(() => Target<object, object, object>(null!, Mock.Of<IArgumentExistenceRecorderMappingRegistrator<object, object>>()));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void NullRegistrator_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target(Mock.Of<IArgumentExistenceRecorderMappingRepository<object, object, object>>(), null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void ValidArguments_ReturnsMapper()
    {
        var mapper = Mock.Of<IArgumentExistenceRecorderMapper<object, object>>();
        Mock<IArgumentExistenceRecorderMappingRepository<object, object, object>> repositoryMock = new() { DefaultValue = DefaultValue.Mock };

        repositoryMock.Setup(static (repository) => repository.Builder.Build()).Returns(mapper);

        var result = Target(repositoryMock.Object, Mock.Of<IArgumentExistenceRecorderMappingRegistrator<object, object>>());

        Assert.Same(mapper, result);
    }

    private IArgumentExistenceRecorderMapper<TParameter, TRecord> Target<TParameter, TParameterRepresentation, TRecord>(
        IArgumentExistenceRecorderMappingRepository<TParameter, TParameterRepresentation, TRecord> mappingRepository,
        IArgumentExistenceRecorderMappingRegistrator<TParameterRepresentation, TRecord> mappingRegistrator)
    {
        return Fixture.Sut.Create(mappingRepository, mappingRegistrator);
    }
}
