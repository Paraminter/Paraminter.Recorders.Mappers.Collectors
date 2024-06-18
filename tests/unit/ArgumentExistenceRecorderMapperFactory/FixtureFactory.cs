namespace Paraminter.Recorders.Mappers.Collectors;

internal static class FixtureFactory
{
    public static IFixture Create()
    {
        ArgumentExistenceRecorderMapperFactory sut = new();

        return new Fixture(sut);
    }

    private sealed class Fixture
        : IFixture
    {
        private readonly IArgumentExistenceRecorderMapperFactory Sut;

        public Fixture(
            IArgumentExistenceRecorderMapperFactory sut)
        {
            Sut = sut;
        }

        IArgumentExistenceRecorderMapperFactory IFixture.Sut => Sut;
    }
}
