namespace Paraminter.Recorders.Mappers.Collectors;

using Xunit;

public sealed class Constructor
{
    [Fact]
    public void ReturnsFactory()
    {
        var result = Target();

        Assert.NotNull(result);
    }

    private static ArgumentDataRecorderMappingRepositoryFactory Target() => new();
}
