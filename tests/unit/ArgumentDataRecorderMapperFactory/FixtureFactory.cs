namespace Paraminter.Recorders.Mappers.Collectors;

internal static class FixtureFactory
{
    public static IFixture Create()
    {
        ArgumentDataRecorderMapperFactory sut = new();

        return new Fixture(sut);
    }

    private sealed class Fixture
        : IFixture
    {
        private readonly IArgumentDataRecorderMapperFactory Sut;

        public Fixture(
            IArgumentDataRecorderMapperFactory sut)
        {
            Sut = sut;
        }

        IArgumentDataRecorderMapperFactory IFixture.Sut => Sut;
    }
}
