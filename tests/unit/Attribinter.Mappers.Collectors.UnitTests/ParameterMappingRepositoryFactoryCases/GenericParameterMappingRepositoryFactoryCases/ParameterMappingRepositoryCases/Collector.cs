namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.GenericParameterMappingRepositoryFactoryCases.ParameterMappingRepositoryCases;

using Xunit;

public sealed class Collector
{
    private static IParameterMappingCollector<TParameter, TRecord, TData> Target<TParameter, TRecord, TData>(IParameterMappingRepository<TParameter, TRecord, TData> repository) => repository.Collector;

    [Fact]
    public void Valid_ReturnsNotNull()
    {
        var context = RepositoryContext<object, object, object>.Create();

        var actual = Target(context.Repository);

        Assert.NotNull(actual);
    }
}
