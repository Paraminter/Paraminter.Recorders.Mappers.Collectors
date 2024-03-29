namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.T2;

using Moq;

internal sealed class FactoryContext<TParameter, TParameterRepresentation>
{
    public static FactoryContext<TParameter, TParameterRepresentation> Create()
    {
        IParameterMappingRepositoryFactory factory = new ParameterMappingRepositoryFactory();

        var parameterRepresentationFactory = Mock.Of<IParameterRepresentationFactory<TParameter, TParameterRepresentation>>();
        var parameterRepresentationComparer = Mock.Of<IParameterRepresentationEqualityComparer<TParameterRepresentation>>();

        var specificFactory = factory.WithParameterRepresentation(parameterRepresentationFactory, parameterRepresentationComparer);

        return new(specificFactory);
    }

    public IParameterMappingRepositoryFactory<TParameter, TParameterRepresentation> Factory { get; }

    private FactoryContext(IParameterMappingRepositoryFactory<TParameter, TParameterRepresentation> factory)
    {
        Factory = factory;
    }
}
