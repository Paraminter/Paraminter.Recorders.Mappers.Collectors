namespace Paraminter.Recorders.Mappers.Collectors;

internal static class FixtureFactory
{
    public static IFixture Create()
    {
        IArgumentDataRecorderMappingRepositoryFactory sut = new ArgumentDataRecorderMappingRepositoryFactory();

        return new Fixture(sut);
    }

    private sealed class Fixture
        : IFixture
    {
        private readonly IArgumentDataRecorderMappingRepositoryFactory Sut;

        public Fixture(
            IArgumentDataRecorderMappingRepositoryFactory sut)
        {
            Sut = sut;
        }

        IArgumentDataRecorderMappingRepositoryFactory IFixture.Sut => Sut;
    }
}
