namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases;

internal static class FactoryFixtureFactory
{
    public static IFactoryFixture Create()
    {
        IParameterMappingRepositoryFactory factory = new ParameterMappingRepositoryFactory();

        return new FactoryFixture(factory);
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
