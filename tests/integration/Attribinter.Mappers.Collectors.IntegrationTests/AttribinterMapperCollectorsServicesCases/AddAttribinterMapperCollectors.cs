namespace Attribinter.Mappers.Collectors.AttribinterMapperCollectorsServicesCases;

using Attribinter.Parameters.Representations;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Moq;

using System;

using Xunit;

public sealed class AddAttribinterMapperCollectors
{
    private static IServiceCollection Target(IServiceCollection services) => AttribinterMapperCollectorsServices.AddAttribinterMapperCollectors(services);

    [Fact]
    public void NullServiceCollection_ArgumentNullException()
    {
        var exception = Record.Exception(() => Target(null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void ValidServiceCollection_ReturnsSameServiceCollection()
    {
        var serviceCollection = Mock.Of<IServiceCollection>();

        var actual = Target(serviceCollection);

        Assert.Same(serviceCollection, actual);
    }

    [Fact]
    public void IParameterMapperFactory_ServiceCanBeResolved() => ServiceCanBeResolved<IParameterMapperFactory>();

    [Fact]
    public void IParameterMappingRepositoryFactory_T0_ServiceCanBeResolved() => ServiceCanBeResolved<IParameterMappingRepositoryFactory>();

    [Fact]
    public void IParameterMappingRepositoryFactory_T2_RepresentationFactoryAdded_ServiceCanBeResolved()
    {
        ServiceCanBeResolved<IParameterMappingRepositoryFactory<object, object>>(additionalConfiguration);

        static void additionalConfiguration(IServiceCollection services) => services.AddSingleton(Mock.Of<IParameterRepresentationFactory<object, object>>());
    }

    [AssertionMethod]
    private static void ServiceCanBeResolved<TService>() where TService : notnull => ServiceCanBeResolved<TService>((_) => { });

    [AssertionMethod]
    private static void ServiceCanBeResolved<TService>(Action<IServiceCollection> additionalConfiguration) where TService : notnull
    {
        HostBuilder host = new();

        host.ConfigureServices(configureServices);

        var serviceProvider = host.Build().Services;

        var service = serviceProvider.GetRequiredService<TService>();

        Assert.NotNull(service);

        void configureServices(IServiceCollection services)
        {
            Target(services);
            additionalConfiguration(services);
        }
    }
}
