namespace Attribinter.Mappers.Collectors;

using System.Collections.Generic;

/// <summary>Handles creation of <see cref="IParameterMapper{TParameter, TRecord, TData}"/> using <see cref="IParameterMappingRegistrator{TParameter, TRecord, TData}"/>.</summary>
public interface IParameterMapperFactory
{
    /// <summary>Creates a <see cref="IParameterMapper{TParameter, TRecord, TData}"/>, mapping parameters to recorders.</summary>
    /// <typeparam name="TParameter">The type of the mapped parameters.</typeparam>
    /// <typeparam name="TParameterRepresentation">The type used as a representation of the mapped parameters.</typeparam>
    /// <typeparam name="TRecord">The type of the data record to which the mapped recorders record data.</typeparam>
    /// <typeparam name="TData">The type of the data recorded by the mapped recorders.</typeparam>
    /// <param name="registrator">Registers mappings from parameters to recorders.</param>
    /// <param name="parameterRepresentationFactory">Handles creation of representations of the mapped parameters.</param>
    /// <param name="parameterComparer">Determines equality when comparing parameters.</param>
    /// <returns>The created <see cref="IParameterMapper{TParameter, TRecord, TData}"/>.</returns>
    public abstract IParameterMapper<TParameter, TRecord, TData> Create<TParameter, TParameterRepresentation, TRecord, TData>(IParameterMappingRegistrator<TParameterRepresentation, TRecord, TData> registrator, IParameterRepresentationFactory<TParameter, TParameterRepresentation> parameterRepresentationFactory, IEqualityComparer<TParameterRepresentation> parameterComparer);
}
