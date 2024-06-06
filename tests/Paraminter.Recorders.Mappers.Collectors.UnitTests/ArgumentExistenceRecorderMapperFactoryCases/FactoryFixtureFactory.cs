namespace Paraminter.Recorders.Mappers.Collectors.ArgumentExistenceRecorderMapperFactoryCases;

internal static class FactoryFixtureFactory
{
    public static IFactoryFixture Create()
    {
        ArgumentExistenceRecorderMapperFactory sut = new();

        return new FactoryFixture(sut);
    }

    private sealed class FactoryFixture
        : IFactoryFixture
    {
        private readonly IArgumentExistenceRecorderMapperFactory Sut;

        public FactoryFixture(
            IArgumentExistenceRecorderMapperFactory sut)
        {
            Sut = sut;
        }

        IArgumentExistenceRecorderMapperFactory IFactoryFixture.Sut => Sut;
    }
}
