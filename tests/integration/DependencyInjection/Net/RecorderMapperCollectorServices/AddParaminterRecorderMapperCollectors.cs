namespace Paraminter.Recorders.Mappers.Collectors;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Xunit;

public sealed class AddParaminterRecorderMapperCollectors
{
    [Fact]
    public void IArgumentDataRecorderMapperFactory_ServiceCanBeResolved() => ServiceCanBeResolved<IArgumentDataRecorderMapperFactory>();

    [Fact]
    public void IArgumentExistenceRecorderMapperFactory_ServiceCanBeResolved() => ServiceCanBeResolved<IArgumentExistenceRecorderMapperFactory>();

    [Fact]
    public void IArgumentDataRecorderMappingRepositoryFactory_ServiceCanBeResolved() => ServiceCanBeResolved<IArgumentDataRecorderMappingRepositoryFactory>();

    [Fact]
    public void IArgumentExistenceRecorderMappingRepositoryFactory_ServiceCanBeResolved() => ServiceCanBeResolved<IArgumentExistenceRecorderMappingRepositoryFactory>();

    private static void Target(
        IServiceCollection services)
    {
        RecorderMapperCollectorServices.AddParaminterRecorderMapperCollectors(services);
    }

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
