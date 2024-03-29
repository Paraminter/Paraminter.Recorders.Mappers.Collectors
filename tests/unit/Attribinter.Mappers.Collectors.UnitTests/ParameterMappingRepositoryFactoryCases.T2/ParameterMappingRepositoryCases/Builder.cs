namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.T2.ParameterMappingRepositoryCases;

using Xunit;

public sealed class Builder
{
    private static IParameterMapperBuilder<TParameter, TRecord, TData> Target<TParameter, TParameterRepresentation, TRecord, TData>(IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TData> repository) => repository.Builder;

    [Fact]
    public void Valid_ReturnsNotNull()
    {
        var context = RepositoryContext<object, object, object, object>.Create();

        var actual = Target(context.Repository);

        Assert.NotNull(actual);
    }
}
