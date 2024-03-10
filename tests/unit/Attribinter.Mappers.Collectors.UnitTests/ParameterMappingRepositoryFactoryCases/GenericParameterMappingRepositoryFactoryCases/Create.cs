namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.GenericParameterMappingRepositoryFactoryCases;

using Xunit;

public sealed class Create
{
    private static IParameterMappingRepository<TParameter, TRecord, TData> Target<TParameter, TRecord, TData>(IParameterMappingRepositoryFactory<TParameter> factory) => factory.Create<TRecord, TData>();

    [Fact]
    public void Valid_ReturnsNotNull()
    {
        var context = FactoryContext<object>.Create();

        var actual = Target<object, object, object>(context.Factory);

        Assert.NotNull(actual);
    }
}
