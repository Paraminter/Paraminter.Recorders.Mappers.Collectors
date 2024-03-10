namespace Attribinter.Mappers.Collectors.AttribinterMapperCollectorsServicesCases;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Moq;

using System;

using Xunit;

public sealed class AddAttribinterMapperCollectors
{
    private static IServiceCollection Target(IServiceCollection services) => AttribinterMapperCollectorsServices.AddAttribinterMapperCollectors(services);

    private readonly IServiceProvider ServiceProvider;

    public AddAttribinterMapperCollectors()
    {
        HostBuilder host = new();

        host.ConfigureServices(static (services) => Target(services));

        ServiceProvider = host.Build().Services;
    }

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
    public void IParameterMappingRepositoryFactory_ServiceCanBeResolved() => ServiceCanBeResolved<IParameterMappingRepositoryFactory>();

    [Fact]
    public void IParameterMapperFactory_ServiceCanBeResolved() => ServiceCanBeResolved<IParameterMapperFactory>();

    [AssertionMethod]
    private void ServiceCanBeResolved<TService>() where TService : notnull
    {
        var service = ServiceProvider.GetRequiredService<TService>();

        Assert.NotNull(service);
    }
}
