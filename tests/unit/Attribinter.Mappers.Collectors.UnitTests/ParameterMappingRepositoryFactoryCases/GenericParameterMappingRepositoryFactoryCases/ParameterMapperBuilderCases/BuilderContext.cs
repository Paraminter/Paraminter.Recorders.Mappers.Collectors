namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.GenericParameterMappingRepositoryFactoryCases.ParameterMapperBuilderCases;

using Moq;

using System.Collections.Generic;

internal sealed class BuilderContext<TParameter, TRecord, TData>
{
    public static BuilderContext<TParameter, TRecord, TData> Create()
    {
        ParameterMappingRepositoryFactory factory = new();

        var parameterComparer = Mock.Of<IEqualityComparer<TParameter>>();

        var genericFactory = ((IParameterMappingRepositoryFactory)factory).ForParameter(parameterComparer);

        var repository = genericFactory.Create<TRecord, TData>();

        return new(repository.Builder);
    }

    public IParameterMapperBuilder<TParameter, TRecord, TData> Builder { get; }

    public BuilderContext(IParameterMapperBuilder<TParameter, TRecord, TData> builder)
    {
        Builder = builder;
    }
}
