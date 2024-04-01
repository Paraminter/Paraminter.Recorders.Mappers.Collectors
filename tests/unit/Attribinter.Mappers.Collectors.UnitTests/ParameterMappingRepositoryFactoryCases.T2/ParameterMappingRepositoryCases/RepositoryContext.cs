namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.T2.ParameterMappingRepositoryCases;

using Attribinter.Parameters.Representations;

using Moq;

using System.Collections.Generic;

internal sealed class RepositoryContext<TParameter, TParameterRepresentation, TRecord, TData>
{
    public static RepositoryContext<TParameter, TParameterRepresentation, TRecord, TData> Create()
    {
        IParameterMappingRepositoryFactory factory = new ParameterMappingRepositoryFactory();

        var parameterRepresentationFactory = Mock.Of<IParameterRepresentationFactory<TParameter, TParameterRepresentation>>();
        var parameterRepresentationComparer = Mock.Of<IEqualityComparer<TParameterRepresentation>>();

        var repository = factory.WithParameterRepresentation(parameterRepresentationFactory).Create<TRecord, TData>(parameterRepresentationComparer);

        return new(repository);
    }

    public IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TData> Repository { get; }

    public RepositoryContext(IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TData> repository)
    {
        Repository = repository;
    }
}
