namespace Attribinter.Mappers.Collectors.ParameterMapperFactoryCases;

using Moq;

using System;
using System.Collections.Generic;

using Xunit;

public sealed class Create
{
    private IParameterMapper<TParameter, TRecord, TData> Target<TParameter, TParameterRepresentation, TRecord, TData>(IParameterMappingRegistrator<TParameterRepresentation, TRecord, TData> registrator, IParameterRepresentationFactory<TParameter, TParameterRepresentation> parameterRepresentationFactory, IEqualityComparer<TParameterRepresentation> parameterComparer) => Target(Context.Factory, registrator, parameterRepresentationFactory, parameterComparer);
    private static IParameterMapper<TParameter, TRecord, TData> Target<TParameter, TParameterRepresentation, TRecord, TData>(IParameterMapperFactory factory, IParameterMappingRegistrator<TParameterRepresentation, TRecord, TData> registrator, IParameterRepresentationFactory<TParameter, TParameterRepresentation> parameterRepresentationFactory, IEqualityComparer<TParameterRepresentation> parameterComparer) => factory.Create(registrator, parameterRepresentationFactory, parameterComparer);

    private readonly FactoryContext Context = FactoryContext.Create();

    [Fact]
    public void NullRegistrator_ThrowsArgumentNullException()
    {
        var exception = Record.Exception(() => Target<object, object, object, object>(null!, Mock.Of<IParameterRepresentationFactory<object, object>>(), Mock.Of<IEqualityComparer<object>>()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void NullParameterRepresentationFactory_ThrowsArgumentNullException()
    {
        var exception = Record.Exception(() => Target<object, object, object, object>(Mock.Of<IParameterMappingRegistrator<object, object, object>>(), null!, Mock.Of<IEqualityComparer<object>>()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void NullParameterComparer_ThrowsArgumentNullException()
    {
        var exception = Record.Exception(() => Target(Mock.Of<IParameterMappingRegistrator<object, object, object>>(), Mock.Of<IParameterRepresentationFactory<object, object>>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void ValidArguments_ReturnsMapper()
    {
        var mapper = Mock.Of<IParameterMapper<object, object, object>>();
        var collector = Mock.Of<IParameterMappingCollector<object, object, object>>();
        Mock<IParameterMappingRepository<object, object, object, object>> repositoryMock = new() { DefaultValue = DefaultValue.Mock };
        Mock<IParameterMappingRegistrator<object, object, object>> registratorMock = new();
        var parameterRepresentationFactory = Mock.Of<IParameterRepresentationFactory<object, object>>();
        var parameterComparer = Mock.Of<IEqualityComparer<object>>();

        repositoryMock.Setup(static (repository) => repository.Builder.Build()).Returns(mapper);
        repositoryMock.Setup(static (repository) => repository.Collector).Returns(collector);

        Context.RepositoryFactoryMock.Setup(static (factory) => factory.ForParameter(It.IsAny<IParameterRepresentationFactory<object, object>>(), It.IsAny<IEqualityComparer<object>>()).Create<object, object>()).Returns(repositoryMock.Object);

        var actual = Target(registratorMock.Object, parameterRepresentationFactory, parameterComparer);

        Assert.NotNull(actual);

        repositoryMock.Verify(static (repository) => repository.Builder.Build(), Times.Once());
        repositoryMock.Verify(static (repository) => repository.Collector, Times.Once());
        repositoryMock.VerifyNoOtherCalls();

        registratorMock.Verify((registrator) => registrator.Register(collector), Times.Once());
        registratorMock.VerifyNoOtherCalls();

        Context.RepositoryFactoryMock.Verify((factory) => factory.ForParameter(parameterRepresentationFactory, parameterComparer).Create<object, object>(), Times.Once());
        Context.RepositoryFactoryMock.VerifyNoOtherCalls();
    }
}
