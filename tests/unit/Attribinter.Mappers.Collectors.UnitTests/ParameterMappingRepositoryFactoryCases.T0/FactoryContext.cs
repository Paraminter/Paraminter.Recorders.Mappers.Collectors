namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.T0;

internal sealed class FactoryContext
{
    public static FactoryContext Create()
    {
        IParameterMappingRepositoryFactory factory = new ParameterMappingRepositoryFactory();

        return new(factory);
    }

    public IParameterMappingRepositoryFactory Factory { get; }

    private FactoryContext(IParameterMappingRepositoryFactory factory)
    {
        Factory = factory;
    }
}
