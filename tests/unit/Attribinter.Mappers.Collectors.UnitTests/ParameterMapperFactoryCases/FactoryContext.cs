namespace Attribinter.Mappers.Collectors.ParameterMapperFactoryCases;

internal sealed class FactoryContext
{
    public static FactoryContext Create()
    {
        IParameterMapperFactory factory = new ParameterMapperFactory();

        return new(factory);
    }

    public IParameterMapperFactory Factory { get; }

    private FactoryContext(IParameterMapperFactory factory)
    {
        Factory = factory;
    }
}
