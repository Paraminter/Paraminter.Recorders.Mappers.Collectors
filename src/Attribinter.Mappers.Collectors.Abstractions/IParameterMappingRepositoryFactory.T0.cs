namespace Attribinter.Mappers.Collectors;

using Attribinter.Parameters.Representations;

/// <summary>Handles creation of <see cref="IParameterMappingRepository{TParameter, TParameterRepresentation, TRecord, TData}"/>.</summary>
public interface IParameterMappingRepositoryFactory
{
    /// <summary>Specifies how the mapped parameters should be represented.</summary>
    /// <typeparam name="TParameter">The type of the mapped parameters.</typeparam>
    /// <typeparam name="TParameterRepresentation">The type used as a representation of the mapped parameters.</typeparam>
    /// <param name="parameterRepresentationFactory">Handles creation of parameter representations.</param>
    /// <returns>A <see cref="IParameterMappingRepositoryFactory{TParameter, TParameterRepresentation}"/>, handling creation of <see cref="IParameterMappingRepository{TParameter, TParameterRepresentation, TRecord, TData}"/>.</returns>
    public abstract IParameterMappingRepositoryFactory<TParameter, TParameterRepresentation> WithParameterRepresentation<TParameter, TParameterRepresentation>(IParameterRepresentationFactory<TParameter, TParameterRepresentation> parameterRepresentationFactory);
}
