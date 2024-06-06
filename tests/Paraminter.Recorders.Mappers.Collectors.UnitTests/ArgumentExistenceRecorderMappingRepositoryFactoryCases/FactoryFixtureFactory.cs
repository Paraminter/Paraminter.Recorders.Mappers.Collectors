namespace Paraminter.Recorders.Mappers.Collectors.ArgumentExistenceRecorderMappingRepositoryFactoryCases;

internal static class FactoryFixtureFactory
{
    public static IFactoryFixture Create()
    {
        IArgumentExistenceRecorderMappingRepositoryFactory sut = new ArgumentExistenceRecorderMappingRepositoryFactory();

        return new FactoryFixture(sut);
    }

    private sealed class FactoryFixture
        : IFactoryFixture
    {
        private readonly IArgumentExistenceRecorderMappingRepositoryFactory Sut;

        public FactoryFixture(
            IArgumentExistenceRecorderMappingRepositoryFactory sut)
        {
            Sut = sut;
        }

        IArgumentExistenceRecorderMappingRepositoryFactory IFactoryFixture.Sut => Sut;
    }
}
