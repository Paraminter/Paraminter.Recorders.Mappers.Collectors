namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases;

internal static class FactoryFixtureFactory
{
    public static IFactoryFixture Create()
    {
        IParameterMappingRepositoryFactory sut = new ParameterMappingRepositoryFactory();

        return new FactoryFixture(sut);
    }

    private sealed class FactoryFixture : IFactoryFixture
    {
        private readonly IParameterMappingRepositoryFactory Sut;

        public FactoryFixture(IParameterMappingRepositoryFactory sut)
        {
            Sut = sut;
        }

        IParameterMappingRepositoryFactory IFactoryFixture.Sut => Sut;
    }
}
