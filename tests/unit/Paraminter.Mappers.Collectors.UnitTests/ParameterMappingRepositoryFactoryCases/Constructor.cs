namespace Paraminter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases;

using Xunit;

public sealed class Constructor
{
    [Fact]
    public void ReturnsFactory()
    {
        var result = Target();

        Assert.NotNull(result);
    }

    private static ParameterMappingRepositoryFactory Target() => new();
}
