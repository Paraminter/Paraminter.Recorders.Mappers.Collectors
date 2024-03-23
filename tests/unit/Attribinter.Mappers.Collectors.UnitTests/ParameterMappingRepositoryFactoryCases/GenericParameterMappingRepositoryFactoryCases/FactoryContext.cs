namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.GenericParameterMappingRepositoryFactoryCases;

using Moq;

using System.Collections.Generic;

internal sealed class FactoryContext<TParameter, TParameterRepresentation>
{
    public static FactoryContext<TParameter, TParameterRepresentation> Create()
    {
        ParameterMappingRepositoryFactory nonGenericFactory = new();

        var parameterRepresentationFactory = Mock.Of<IParameterRepresentationFactory<TParameter, TParameterRepresentation>>();
        var parameterComparer = Mock.Of<IEqualityComparer<TParameterRepresentation>>();

        var factory = ((IParameterMappingRepositoryFactory)nonGenericFactory).ForParameter(parameterRepresentationFactory, parameterComparer);

        return new(factory);
    }

    public IParameterMappingRepositoryFactory<TParameter, TParameterRepresentation> Factory { get; }

    private FactoryContext(IParameterMappingRepositoryFactory<TParameter, TParameterRepresentation> factory)
    {
        Factory = factory;
    }
}
