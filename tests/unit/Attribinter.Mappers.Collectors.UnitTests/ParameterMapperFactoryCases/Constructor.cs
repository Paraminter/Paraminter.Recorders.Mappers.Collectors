namespace Attribinter.Mappers.Collectors.ParameterMapperFactoryCases;

using Xunit;

public sealed class Constructor
{
    private static ParameterMapperFactory Target() => new();

    [Fact]
    public void ReturnsFactory()
    {
        var result = Target();

        Assert.NotNull(result);
    }
}
