namespace Paraminter.Recorders.Mappers.Collectors;

using Paraminter.Parameters.Representations;

using System.Collections.Generic;

/// <summary>Handles creation of <see cref="IArgumentExistenceRecorderMappingRepository{TParameter, TParameterRepresentation, TRecord}"/>.</summary>
public interface IArgumentExistenceRecorderMappingRepositoryFactory
{
    /// <summary>Creates a <see cref="IArgumentExistenceRecorderMappingRepository{TParameter, TParameterRepresentation, TRecord}"/>.</summary>
    /// <typeparam name="TParameter">The type of the mapped parameters.</typeparam>
    /// <typeparam name="TParameterRepresentation">The type used as a representation of the mapped parameters.</typeparam>
    /// <typeparam name="TRecord">The type of the data record to which the mapped recorders record data.</typeparam>
    /// <param name="parameterRepresentationFactory">Handles creation of parameter representations.</param>
    /// <param name="parameterRepresentationComparer">Determines equality when comparing parameter representations.</param>
    /// <returns>The created <see cref="IArgumentExistenceRecorderMappingRepository{TParameter, TParameterRepresentation, TRecord}"/>.</returns>
    public abstract IArgumentExistenceRecorderMappingRepository<TParameter, TParameterRepresentation, TRecord> Create<TParameter, TParameterRepresentation, TRecord>(
        IParameterRepresentationFactory<TParameter, TParameterRepresentation> parameterRepresentationFactory,
        IEqualityComparer<TParameterRepresentation> parameterRepresentationComparer);
}
