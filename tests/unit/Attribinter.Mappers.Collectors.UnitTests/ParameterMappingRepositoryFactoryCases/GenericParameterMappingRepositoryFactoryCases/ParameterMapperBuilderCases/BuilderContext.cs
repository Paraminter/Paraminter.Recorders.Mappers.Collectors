namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.GenericParameterMappingRepositoryFactoryCases.ParameterMapperBuilderCases;

using Moq;

using System.Collections.Generic;

internal sealed class BuilderContext<TParameter, TParameterRepresentation, TRecord, TData>
{
    public static BuilderContext<TParameter, TParameterRepresentation, TRecord, TData> Create()
    {
        ParameterMappingRepositoryFactory factory = new();

        var parameterRepresentationFactory = Mock.Of<IParameterRepresentationFactory<TParameter, TParameterRepresentation>>();
        var parameterComparer = Mock.Of<IEqualityComparer<TParameterRepresentation>>();

        var genericFactory = ((IParameterMappingRepositoryFactory)factory).ForParameter(parameterRepresentationFactory, parameterComparer);

        var repository = genericFactory.Create<TRecord, TData>();

        return new(repository.Builder);
    }

    public IParameterMapperBuilder<TParameter, TRecord, TData> Builder { get; }

    public BuilderContext(IParameterMapperBuilder<TParameter, TRecord, TData> builder)
    {
        Builder = builder;
    }
}
