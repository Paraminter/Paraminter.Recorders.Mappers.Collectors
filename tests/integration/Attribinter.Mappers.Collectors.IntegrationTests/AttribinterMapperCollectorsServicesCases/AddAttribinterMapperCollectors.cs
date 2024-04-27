namespace Attribinter.Mappers.Collectors.AttribinterMapperCollectorsServicesCases;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Moq;

using System;

using Xunit;

public sealed class AddAttribinterMapperCollectors
{
    [Fact]
    public void NullServiceCollection_ArgumentNullException()
    {
        var result = Record.Exception(() => Target(null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void ValidServiceCollection_ReturnsSameServiceCollection()
    {
        var services = Mock.Of<IServiceCollection>();

        var result = Target(services);

        Assert.Same(services, result);
    }

    [Fact]
    public void IParameterMapperFactory_ServiceCanBeResolved() => ServiceCanBeResolved<IParameterMapperFactory>();

    [Fact]
    public void IParameterMappingRepositoryFactory_ServiceCanBeResolved() => ServiceCanBeResolved<IParameterMappingRepositoryFactory>();

    private static IServiceCollection Target(IServiceCollection services) => AttribinterMapperCollectorsServices.AddAttribinterMapperCollectors(services);

    [AssertionMethod]
    private static void ServiceCanBeResolved<TService>()
        where TService : notnull
    {
        HostBuilder host = new();

        host.ConfigureServices(static (services) => Target(services));

        var serviceProvider = host.Build().Services;

        var result = serviceProvider.GetRequiredService<TService>();

        Assert.NotNull(result);
    }
}
