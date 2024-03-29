namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.T2.ParameterMapperBuilderCases;

using Moq;

internal sealed class BuilderContext<TParameter, TParameterRepresentation, TRecord, TData>
{
    public static BuilderContext<TParameter, TParameterRepresentation, TRecord, TData> Create()
    {
        IParameterMappingRepositoryFactory factory = new ParameterMappingRepositoryFactory();

        var parameterRepresentationFactory = Mock.Of<IParameterRepresentationFactory<TParameter, TParameterRepresentation>>();
        var parameterRepresentationComparer = Mock.Of<IParameterRepresentationEqualityComparer<TParameterRepresentation>>();

        var repository = factory.WithParameterRepresentation(parameterRepresentationFactory, parameterRepresentationComparer).Create<TRecord, TData>();

        return new(repository.Builder);
    }

    public IParameterMapperBuilder<TParameter, TRecord, TData> Builder { get; }

    public BuilderContext(IParameterMapperBuilder<TParameter, TRecord, TData> builder)
    {
        Builder = builder;
    }
}
