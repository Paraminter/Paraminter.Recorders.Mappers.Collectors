namespace Attribinter.Mappers.Collectors;

/// <summary>Handles creation of <see cref="IParameterMapper{TParameter, TRecord, TData}"/> using <see cref="IParameterMappingRepository{TParameter, TParameterRepresentation, TRecord, TData}"/>.</summary>
public interface IParameterMapperFactory
{
    /// <summary>Creates a <see cref="IParameterMapper{TParameter, TRecord, TData}"/>, mapping parameters to recorders.</summary>
    /// <typeparam name="TParameter">The type of the mapped parameters.</typeparam>
    /// <typeparam name="TParameterRepresentation">The type used as a representation of the mapped parameters.</typeparam>
    /// <typeparam name="TRecord">The type of the data record to which the mapped recorders record data.</typeparam>
    /// <typeparam name="TData">The type of the data recorded by the mapped recorders.</typeparam>
    /// <param name="parameterMappingRepository">A repository of mappings from parameters to recorders.</param>
    /// <param name="parameterMappingRegistrator">Registers mappings from parameters to recorders.</param>
    /// <returns>The created <see cref="IParameterMapper{TParameter, TRecord, TData}"/>.</returns>
    public abstract IParameterMapper<TParameter, TRecord, TData> Create<TParameter, TParameterRepresentation, TRecord, TData>(IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TData> parameterMappingRepository, IParameterMappingRegistrator<TParameterRepresentation, TRecord, TData> parameterMappingRegistrator);
}
