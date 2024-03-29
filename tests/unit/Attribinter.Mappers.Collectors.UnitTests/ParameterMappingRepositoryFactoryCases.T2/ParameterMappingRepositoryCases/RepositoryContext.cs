namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.T2.ParameterMappingRepositoryCases;

using Moq;

internal sealed class RepositoryContext<TParameter, TParameterRepresentation, TRecord, TData>
{
    public static RepositoryContext<TParameter, TParameterRepresentation, TRecord, TData> Create()
    {
        IParameterMappingRepositoryFactory factory = new ParameterMappingRepositoryFactory();

        var parameterRepresentationFactory = Mock.Of<IParameterRepresentationFactory<TParameter, TParameterRepresentation>>();
        var parameterComparer = Mock.Of<IParameterRepresentationEqualityComparer<TParameterRepresentation>>();

        var repository = factory.WithParameterRepresentation(parameterRepresentationFactory, parameterComparer).Create<TRecord, TData>();

        return new(repository);
    }

    public IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TData> Repository { get; }

    public RepositoryContext(IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TData> repository)
    {
        Repository = repository;
    }
}
