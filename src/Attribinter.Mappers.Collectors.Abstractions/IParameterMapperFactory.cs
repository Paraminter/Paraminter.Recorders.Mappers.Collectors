namespace Attribinter.Mappers.Collectors;

using System.Collections.Generic;

/// <summary>Handles creation of <see cref="IParameterMapper{TParameter, TRecord, TData}"/> using <see cref="IParameterMappingRegistrator{TKey, TRecord, TData}"/>.</summary>
public interface IParameterMapperFactory
{
    /// <summary>Creates a <see cref="IParameterMapper{TParameter, TRecord, TData}"/>, mapping parameters to recorders.</summary>
    /// <typeparam name="TParameter">The type of the mapped parameters.</typeparam>
    /// <typeparam name="TRecord">The type of the data record to which the mapped recorders record data.</typeparam>
    /// <typeparam name="TData">The type of the data recorded by the mapped recorders.</typeparam>
    /// <param name="registrator">Registers mappings from parameters to recorders.</param>
    /// <param name="parameterComparer">Determines equality when comparing parameters.</param>
    /// <returns>The created <see cref="IParameterMapper{TParameter, TRecord, TData}"/>.</returns>
    public abstract IParameterMapper<TParameter, TRecord, TData> Create<TParameter, TRecord, TData>(IParameterMappingRegistrator<TParameter, TRecord, TData> registrator, IEqualityComparer<TParameter> parameterComparer);
}
