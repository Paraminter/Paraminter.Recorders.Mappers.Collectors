namespace Paraminter.Mappers.Collectors.ParaminterMapperCollectorsServicesCases;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Xunit;

public sealed class AddParaminterMapperCollectors
{
    [Fact]
    public void IParameterMapperFactory_ServiceCanBeResolved() => ServiceCanBeResolved<IParameterMapperFactory>();

    [Fact]
    public void IParameterMappingRepositoryFactory_ServiceCanBeResolved() => ServiceCanBeResolved<IParameterMappingRepositoryFactory>();

    private static void Target(IServiceCollection services) => ParaminterMapperCollectorsServices.AddParaminterMapperCollectors(services);

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
