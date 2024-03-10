namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.GenericParameterMappingRepositoryFactoryCases;

using Moq;

using System.Collections.Generic;

internal sealed class FactoryContext<TParameter>
{
    public static FactoryContext<TParameter> Create()
    {
        ParameterMappingRepositoryFactory nonGenericFactory = new();

        var parameterComparer = Mock.Of<IEqualityComparer<TParameter>>();

        var factory = ((IParameterMappingRepositoryFactory)nonGenericFactory).ForParameter(parameterComparer);

        return new(factory);
    }

    public IParameterMappingRepositoryFactory<TParameter> Factory { get; }

    private FactoryContext(IParameterMappingRepositoryFactory<TParameter> factory)
    {
        Factory = factory;
    }
}
