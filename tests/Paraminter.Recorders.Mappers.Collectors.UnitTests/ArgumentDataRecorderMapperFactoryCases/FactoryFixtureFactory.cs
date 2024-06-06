namespace Paraminter.Recorders.Mappers.Collectors.ArgumentDataRecorderMapperFactoryCases;

internal static class FactoryFixtureFactory
{
    public static IFactoryFixture Create()
    {
        ArgumentDataRecorderMapperFactory sut = new();

        return new FactoryFixture(sut);
    }

    private sealed class FactoryFixture
        : IFactoryFixture
    {
        private readonly IArgumentDataRecorderMapperFactory Sut;

        public FactoryFixture(
            IArgumentDataRecorderMapperFactory sut)
        {
            Sut = sut;
        }

        IArgumentDataRecorderMapperFactory IFactoryFixture.Sut => Sut;
    }
}
