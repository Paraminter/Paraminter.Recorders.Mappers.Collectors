namespace Paraminter.Recorders.Mappers.Collectors.ArgumentExistenceRecorderMapperFactoryCases;

using Xunit;

public sealed class Constructor
{
    [Fact]
    public void ReturnsFactory()
    {
        var result = Target();

        Assert.NotNull(result);
    }

    private static ArgumentExistenceRecorderMapperFactory Target() => new();
}
