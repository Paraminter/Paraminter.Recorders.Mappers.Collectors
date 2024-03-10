namespace Attribinter.Mappers.Collectors.ParameterMapperFactoryCases;

using Moq;

using System;
using System.Collections.Generic;

using Xunit;

public sealed class Create
{
    private IParameterMapper<TParameter, TRecord, TData> Target<TParameter, TRecord, TData>(IParameterMappingRegistrator<TParameter, TRecord, TData> registrator, IEqualityComparer<TParameter> parameterComparer) => Target(Context.Factory, registrator, parameterComparer);
    private static IParameterMapper<TParameter, TRecord, TData> Target<TParameter, TRecord, TData>(IParameterMapperFactory factory, IParameterMappingRegistrator<TParameter, TRecord, TData> registrator, IEqualityComparer<TParameter> parameterComparer) => factory.Create(registrator, parameterComparer);

    private readonly FactoryContext Context = FactoryContext.Create();

    [Fact]
    public void NullRegistrator_ThrowsArgumentNullException()
    {
        var exception = Record.Exception(() => Target<object, object, object>(null!, Mock.Of<IEqualityComparer<object>>()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void NullParameterComparer_ThrowsArgumentNullException()
    {
        var exception = Record.Exception(() => Target(Mock.Of<IParameterMappingRegistrator<object, object, object>>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void ValidRegistratorAndParameterComparer_ReturnsMapper()
    {
        var mapper = Mock.Of<IParameterMapper<object, object, object>>();
        var collector = Mock.Of<IParameterMappingCollector<object, object, object>>();
        Mock<IParameterMappingRepository<object, object, object>> repositoryMock = new() { DefaultValue = DefaultValue.Mock };
        Mock<IParameterMappingRegistrator<object, object, object>> registratorMock = new();
        var parameterComparer = Mock.Of<IEqualityComparer<object>>();

        repositoryMock.Setup(static (repository) => repository.Builder.Build()).Returns(mapper);
        repositoryMock.Setup(static (repository) => repository.Collector).Returns(collector);

        Context.RepositoryFactoryMock.Setup(static (factory) => factory.ForParameter(It.IsAny<IEqualityComparer<object>>()).Create<object, object>()).Returns(repositoryMock.Object);

        var actual = Target(registratorMock.Object, parameterComparer);

        Assert.NotNull(actual);

        repositoryMock.Verify(static (repository) => repository.Builder.Build(), Times.Once());
        repositoryMock.Verify(static (repository) => repository.Collector, Times.Once());
        repositoryMock.VerifyNoOtherCalls();

        registratorMock.Verify((registrator) => registrator.Register(collector), Times.Once());
        registratorMock.VerifyNoOtherCalls();

        Context.RepositoryFactoryMock.Verify((factory) => factory.ForParameter(parameterComparer).Create<object, object>(), Times.Once());
        Context.RepositoryFactoryMock.VerifyNoOtherCalls();
    }
}
