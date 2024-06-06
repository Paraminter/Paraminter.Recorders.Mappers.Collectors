namespace Paraminter.Recorders.Mappers.Collectors;

using Paraminter.Parameters.Representations;

using System.Collections.Generic;

/// <summary>Handles creation of <see cref="IArgumentDataRecorderMappingRepository{TParameter, TParameterRepresentation, TRecord, TArgumentData}"/>.</summary>
public interface IArgumentDataRecorderMappingRepositoryFactory
{
    /// <summary>Creates a <see cref="IArgumentDataRecorderMappingRepository{TParameter, TParameterRepresentation, TRecord, TArgumentData}"/>.</summary>
    /// <typeparam name="TParameter">The type of the mapped parameters.</typeparam>
    /// <typeparam name="TParameterRepresentation">The type used as a representation of the mapped parameters.</typeparam>
    /// <typeparam name="TRecord">The type of the data record to which the mapped recorders record data.</typeparam>
    /// <typeparam name="TArgumentData">The type of the data recorded by the mapped recorders.</typeparam>
    /// <param name="parameterRepresentationFactory">Handles creation of parameter representations.</param>
    /// <param name="parameterRepresentationComparer">Determines equality when comparing parameter representations.</param>
    /// <returns>The created <see cref="IArgumentDataRecorderMappingRepository{TParameter, TParameterRepresentation, TRecord, TArgumentData}"/>.</returns>
    public abstract IArgumentDataRecorderMappingRepository<TParameter, TParameterRepresentation, TRecord, TArgumentData> Create<TParameter, TParameterRepresentation, TRecord, TArgumentData>(
        IParameterRepresentationFactory<TParameter, TParameterRepresentation> parameterRepresentationFactory,
        IEqualityComparer<TParameterRepresentation> parameterRepresentationComparer);
}
