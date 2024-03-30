namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.T2;

using Attribinter.Parameters.Representations;

using Moq;

internal sealed class FactoryContext<TParameter, TParameterRepresentation>
{
    public static FactoryContext<TParameter, TParameterRepresentation> Create()
    {
        IParameterMappingRepositoryFactory factory = new ParameterMappingRepositoryFactory();

        var parameterRepresentationFactory = Mock.Of<IParameterRepresentationFactory<TParameter, TParameterRepresentation>>();

        var specificFactory = factory.WithParameterRepresentation(parameterRepresentationFactory);

        return new(specificFactory);
    }

    public IParameterMappingRepositoryFactory<TParameter, TParameterRepresentation> Factory { get; }

    private FactoryContext(IParameterMappingRepositoryFactory<TParameter, TParameterRepresentation> factory)
    {
        Factory = factory;
    }
}
