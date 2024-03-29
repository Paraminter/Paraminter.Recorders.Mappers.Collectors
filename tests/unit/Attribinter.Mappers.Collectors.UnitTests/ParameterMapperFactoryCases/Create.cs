namespace Attribinter.Mappers.Collectors.ParameterMapperFactoryCases;

using Moq;

using System;

using Xunit;

public sealed class Create
{
    private static IParameterMapper<TParameter, TRecord, TData> Target<TParameter, TParameterRepresentation, TRecord, TData>(IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TData> parameterMappingRepository, IParameterMappingRegistrator<TParameterRepresentation, TRecord, TData> parameterMappingRegistrator) => Context.Factory.Create(parameterMappingRepository, parameterMappingRegistrator);

    private static readonly FactoryContext Context = FactoryContext.Create();

    [Fact]
    public void NullRepository_ThrowsArgumentNullException()
    {
        var exception = Record.Exception(() => Target<object, object, object, object>(null!, Mock.Of<IParameterMappingRegistrator<object, object, object>>()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void NullRegistrator_ThrowsArgumentNullException()
    {
        var exception = Record.Exception(() => Target(Mock.Of<IParameterMappingRepository<object, object, object, object>>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
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

        var actual = Target(repositoryMock.Object, registratorMock.Object);

        Assert.NotNull(actual);

        repositoryMock.Verify(static (repository) => repository.Builder.Build(), Times.Once());
        repositoryMock.Verify(static (repository) => repository.Collector, Times.Once());
        repositoryMock.VerifyNoOtherCalls();

        registratorMock.Verify((registrator) => registrator.Register(collector), Times.Once());
        registratorMock.VerifyNoOtherCalls();
    }
}
