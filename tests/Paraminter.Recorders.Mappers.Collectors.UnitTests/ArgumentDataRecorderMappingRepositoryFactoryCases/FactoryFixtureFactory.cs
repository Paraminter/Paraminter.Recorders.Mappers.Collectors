namespace Paraminter.Recorders.Mappers.Collectors.ArgumentDataRecorderMappingRepositoryFactoryCases;

internal static class FactoryFixtureFactory
{
    public static IFactoryFixture Create()
    {
        IArgumentDataRecorderMappingRepositoryFactory sut = new ArgumentDataRecorderMappingRepositoryFactory();

        return new FactoryFixture(sut);
    }

    private sealed class FactoryFixture
        : IFactoryFixture
    {
        private readonly IArgumentDataRecorderMappingRepositoryFactory Sut;

        public FactoryFixture(
            IArgumentDataRecorderMappingRepositoryFactory sut)
        {
            Sut = sut;
        }

        IArgumentDataRecorderMappingRepositoryFactory IFactoryFixture.Sut => Sut;
    }
}
