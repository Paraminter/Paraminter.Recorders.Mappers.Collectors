namespace Paraminter.Mappers.Collectors;

/// <summary>Handles creation of <see cref="IParameterMapper{TParameter, TRecord, TArgumentData}"/> using <see cref="IParameterMappingRepository{TParameter, TParameterRepresentation, TRecord, TArgumentData}"/>.</summary>
public interface IParameterMapperFactory
{
    /// <summary>Creates a <see cref="IParameterMapper{TParameter, TRecord, TArgumentData}"/>, mapping parameters to recorders.</summary>
    /// <typeparam name="TParameter">The type of the mapped parameters.</typeparam>
    /// <typeparam name="TParameterRepresentation">The type used as a representation of the mapped parameters.</typeparam>
    /// <typeparam name="TRecord">The type of the record to which data is recorded.</typeparam>
    /// <typeparam name="TArgumentData">The type representing data about the arguments.</typeparam>
    /// <param name="parameterMappingRepository">A repository of mappings from parameters to recorders.</param>
    /// <param name="parameterMappingRegistrator">Registers mappings from parameters to recorders.</param>
    /// <returns>The created <see cref="IParameterMapper{TParameter, TRecord, TArgumentData}"/>.</returns>
    public abstract IParameterMapper<TParameter, TRecord, TArgumentData> Create<TParameter, TParameterRepresentation, TRecord, TArgumentData>(IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TArgumentData> parameterMappingRepository, IParameterMappingRegistrator<TParameterRepresentation, TRecord, TArgumentData> parameterMappingRegistrator);
}
