namespace Attribinter.Mappers.Collectors.ParameterMapperFactoryCases;

internal static class FactoryFixtureFactory
{
    public static IFactoryFixture Create()
    {
        ParameterMapperFactory sut = new();

        return new FactoryFixture(sut);
    }

    private sealed class FactoryFixture : IFactoryFixture
    {
        private readonly IParameterMapperFactory Sut;

        public FactoryFixture(IParameterMapperFactory sut)
        {
            Sut = sut;
        }

        IParameterMapperFactory IFactoryFixture.Sut => Sut;
    }
}
