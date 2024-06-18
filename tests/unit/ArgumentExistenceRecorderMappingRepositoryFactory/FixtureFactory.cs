namespace Paraminter.Recorders.Mappers.Collectors;

internal static class FixtureFactory
{
    public static IFixture Create()
    {
        IArgumentExistenceRecorderMappingRepositoryFactory sut = new ArgumentExistenceRecorderMappingRepositoryFactory();

        return new Fixture(sut);
    }

    private sealed class Fixture
        : IFixture
    {
        private readonly IArgumentExistenceRecorderMappingRepositoryFactory Sut;

        public Fixture(
            IArgumentExistenceRecorderMappingRepositoryFactory sut)
        {
            Sut = sut;
        }

        IArgumentExistenceRecorderMappingRepositoryFactory IFixture.Sut => Sut;
    }
}
