namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.GenericParameterMappingRepositoryFactoryCases.ParameterMappingRepositoryCases;

using Xunit;

public sealed class Collector
{
    private static IParameterMappingCollector<TParameterRepresentation, TRecord, TData> Target<TParameter, TParameterRepresentation, TRecord, TData>(IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TData> repository) => repository.Collector;

    [Fact]
    public void Valid_ReturnsNotNull()
    {
        var context = RepositoryContext<object, object, object, object>.Create();

        var actual = Target(context.Repository);

        Assert.NotNull(actual);
    }
}
