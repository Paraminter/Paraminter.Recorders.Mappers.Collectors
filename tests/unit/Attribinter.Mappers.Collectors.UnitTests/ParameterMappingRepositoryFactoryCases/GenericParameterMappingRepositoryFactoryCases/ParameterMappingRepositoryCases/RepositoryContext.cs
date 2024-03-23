namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.GenericParameterMappingRepositoryFactoryCases.ParameterMappingRepositoryCases;

using Moq;

using System.Collections.Generic;

internal sealed class RepositoryContext<TParameter, TParameterRepresentation, TRecord, TData>
{
    public static RepositoryContext<TParameter, TParameterRepresentation, TRecord, TData> Create()
    {
        ParameterMappingRepositoryFactory nonGenericFactory = new();

        var parameterRepresentationFactory = Mock.Of<IParameterRepresentationFactory<TParameter, TParameterRepresentation>>();
        var parameterComparer = Mock.Of<IEqualityComparer<TParameterRepresentation>>();

        var genericFactory = ((IParameterMappingRepositoryFactory)nonGenericFactory).ForParameter(parameterRepresentationFactory, parameterComparer);

        var repository = genericFactory.Create<TRecord, TData>();

        return new(repository);
    }

    public IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TData> Repository { get; }

    public RepositoryContext(IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TData> repository)
    {
        Repository = repository;
    }
}
