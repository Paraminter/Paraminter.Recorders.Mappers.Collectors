namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases;

internal sealed class FactoryContext
{
    public static FactoryContext Create()
    {
        ParameterMappingRepositoryFactory factory = new();

        return new(factory);
    }

    public ParameterMappingRepositoryFactory Factory { get; }

    private FactoryContext(ParameterMappingRepositoryFactory factory)
    {
        Factory = factory;
    }
}
