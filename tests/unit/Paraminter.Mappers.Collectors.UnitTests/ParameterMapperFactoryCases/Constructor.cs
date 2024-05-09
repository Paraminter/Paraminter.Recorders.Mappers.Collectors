namespace Paraminter.Mappers.Collectors.ParameterMapperFactoryCases;

using Xunit;

public sealed class Constructor
{
    [Fact]
    public void ReturnsFactory()
    {
        var result = Target();

        Assert.NotNull(result);
    }

    private static ParameterMapperFactory Target() => new();
}
