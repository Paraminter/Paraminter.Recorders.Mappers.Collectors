namespace Attribinter.Mappers.Collectors;

using Attribinter.Parameters.Representations;

using System.Collections.Generic;

/// <summary>Handles creation of <see cref="IParameterMappingRepository{TParameter, TParameterRepresentation, TRecord, TData}"/>.</summary>
public interface IParameterMappingRepositoryFactory
{
    /// <summary>Creates a <see cref="IParameterMappingRepository{TParameter, TParameterRepresentation, TRecord, TData}"/>.</summary>
    /// <typeparam name="TParameter">The type of the mapped parameters.</typeparam>
    /// <typeparam name="TParameterRepresentation">The type used as a representation of the mapped parameters.</typeparam>
    /// <typeparam name="TRecord">The type of the data record to which the mapped recorders record data.</typeparam>
    /// <typeparam name="TData">The type of the data recorded by the mapped recorders.</typeparam>
    /// <param name="parameterRepresentationFactory">Handles creation of parameter representations.</param>
    /// <param name="parameterRepresentationComparer">Determines equality when comparing parameter representations.</param>
    /// <returns>The created <see cref="IParameterMappingRepository{TParameter, TParameterRepresentation, TRecord, TData}"/>.</returns>
    public abstract IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TData> Create<TParameter, TParameterRepresentation, TRecord, TData>(IParameterRepresentationFactory<TParameter, TParameterRepresentation> parameterRepresentationFactory, IEqualityComparer<TParameterRepresentation> parameterRepresentationComparer);
}
