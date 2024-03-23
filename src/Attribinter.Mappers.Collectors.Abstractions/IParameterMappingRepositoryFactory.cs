namespace Attribinter.Mappers.Collectors;

using System.Collections.Generic;

/// <summary>Handles creation of <see cref="IParameterMappingRepository{TParameter, TParameterRepresentation, TRecord, TData}"/>.</summary>
public interface IParameterMappingRepositoryFactory
{
    /// <summary>Specifies what parameter should be mapped by the created <see cref="IParameterMappingRepository{TParameter, TParameterRepresentation, TRecord, TData}"/>.</summary>
    /// <typeparam name="TParameter">The type of the mapped parameters.</typeparam>
    /// <typeparam name="TParameterRepresentation">The type used as a representation of the mapped parameters.</typeparam>
    /// <param name="parameterRepresentationFactory">Handles creation of representations of the mapped parameters.</param>
    /// <param name="parameterComparer">Determines equality when comparing parameters.</param>
    /// <returns>A <see cref="IParameterMappingRepositoryFactory{TParameter, TParameterRepresentation}"/> handling creation of <see cref="IParameterMappingRepository{TParameter, TParameterRepresentation, TRecord, TData}"/>.</returns>
    public abstract IParameterMappingRepositoryFactory<TParameter, TParameterRepresentation> ForParameter<TParameter, TParameterRepresentation>(IParameterRepresentationFactory<TParameter, TParameterRepresentation> parameterRepresentationFactory, IEqualityComparer<TParameterRepresentation> parameterComparer);
}

/// <summary>Handles creation of <see cref="IParameterMappingRepository{TParameter, TParameterRepresentation, TRecord, TData}"/>.</summary>
/// <typeparam name="TParameter">The type of the mapped parameters.</typeparam>
/// <typeparam name="TParameterRepresentation">The type used as a representation of the mapped parameters.</typeparam>
public interface IParameterMappingRepositoryFactory<TParameter, TParameterRepresentation>
{
    /// <summary>Creates a <see cref="IParameterMappingRepository{TParameter, TParameterRepresentation, TRecord, TData}"/>.</summary>
    /// <typeparam name="TRecord">The type of the data record to which the mapped recorders record data.</typeparam>
    /// <typeparam name="TData">The type of the data recorded by the mapped recorders.</typeparam>
    /// <returns>The created <see cref="IParameterMappingRepository{TParameter, TParameterRepresentation, TRecord, TData}"/>.</returns>
    public abstract IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TData> Create<TRecord, TData>();
}
