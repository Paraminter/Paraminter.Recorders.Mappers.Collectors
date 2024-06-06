namespace Paraminter.Recorders.Mappers.Collectors.ArgumentDataRecorderMapperFactoryCases;

using Xunit;

public sealed class Constructor
{
    [Fact]
    public void ReturnsFactory()
    {
        var result = Target();

        Assert.NotNull(result);
    }

    private static ArgumentDataRecorderMapperFactory Target() => new();
}
