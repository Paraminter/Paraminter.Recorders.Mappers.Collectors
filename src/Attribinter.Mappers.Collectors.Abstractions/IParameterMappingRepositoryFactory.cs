namespace Attribinter.Mappers.Collectors;

using System.Collections.Generic;

/// <summary>Handles creation of <see cref="IParameterMappingRepository{TParameter, TRecord, TData}"/>.</summary>
public interface IParameterMappingRepositoryFactory
{
    /// <summary>Specifies what parameter should be mapped by the created <see cref="IParameterMappingRepository{TParameter, TRecord, TData}"/>.</summary>
    /// <typeparam name="TParameter">The type of the mapped parameters.</typeparam>
    /// <param name="parameterComparer">Determines equality when comparing parameters.</param>
    /// <returns>A <see cref="IParameterMappingRepositoryFactory{TParameter}"/> handling creation of <see cref="IParameterMappingRepository{TParameter, TRecord, TData}"/>.</returns>
    public abstract IParameterMappingRepositoryFactory<TParameter> ForParameter<TParameter>(IEqualityComparer<TParameter> parameterComparer);
}

/// <summary>Handles creation of <see cref="IParameterMappingRepository{TParameter, TRecord, TData}"/>.</summary>
/// <typeparam name="TParameter">The type of the mapped parameters.</typeparam>
public interface IParameterMappingRepositoryFactory<TParameter>
{
    /// <summary>Creates a <see cref="IParameterMappingRepository{TParameter, TRecord, TData}"/>.</summary>
    /// <typeparam name="TRecord">The type of the data record to which the mapped recorders record data.</typeparam>
    /// <typeparam name="TData">The type of the data recorded by the mapped recorders.</typeparam>
    /// <returns>The created <see cref="IParameterMappingRepository{TParameter, TRecord, TData}"/>.</returns>
    public abstract IParameterMappingRepository<TParameter, TRecord, TData> Create<TRecord, TData>();
}
