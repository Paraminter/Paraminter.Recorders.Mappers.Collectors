namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases;

using Xunit;

public sealed class Constructor
{
    private static ParameterMappingRepositoryFactory Target() => new();

    [Fact]
    public void ReturnsFactory()
    {
        var result = Target();

        Assert.NotNull(result);
    }
}
