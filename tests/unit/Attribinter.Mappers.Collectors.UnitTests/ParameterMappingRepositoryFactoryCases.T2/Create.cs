namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.T2;

using Xunit;

public sealed class Create
{
    private static IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TData> Target<TParameter, TParameterRepresentation, TRecord, TData>(IParameterMappingRepositoryFactory<TParameter, TParameterRepresentation> factory) => factory.Create<TRecord, TData>();

    [Fact]
    public void Valid_ReturnsNotNull()
    {
        var context = FactoryContext<object, object>.Create();

        var actual = Target<object, object, object, object>(context.Factory);

        Assert.NotNull(actual);
    }
}
