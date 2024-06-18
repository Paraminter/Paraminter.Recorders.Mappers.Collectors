namespace Paraminter.Recorders.Mappers.Collectors;

/// <summary>Handles creation of <see cref="IArgumentDataRecorderMapper{TParameter, TRecord, TArgumentData}"/> using <see cref="IArgumentDataRecorderMappingRepository{TParameter, TParameterRepresentation, TRecord, TArgumentData}"/>.</summary>
public interface IArgumentDataRecorderMapperFactory
{
    /// <summary>Creates a <see cref="IArgumentDataRecorderMapper{TParameter, TRecord, TArgumentData}"/>, mapping parameters to recorders.</summary>
    /// <typeparam name="TParameter">The type of the mapped parameters.</typeparam>
    /// <typeparam name="TParameterRepresentation">The type used as a representation of the mapped parameters.</typeparam>
    /// <typeparam name="TRecord">The type of the record to which data is recorded.</typeparam>
    /// <typeparam name="TArgumentData">The type representing data about the arguments.</typeparam>
    /// <param name="mappingRepository">A repository of mappings from parameters to recorders.</param>
    /// <param name="mappingRegistrator">Registers mappings from parameters to recorders.</param>
    /// <returns>The created <see cref="IArgumentDataRecorderMapper{TParameter, TRecord, TArgumentData}"/>.</returns>
    public abstract IArgumentDataRecorderMapper<TParameter, TRecord, TArgumentData> Create<TParameter, TParameterRepresentation, TRecord, TArgumentData>(
        IArgumentDataRecorderMappingRepository<TParameter, TParameterRepresentation, TRecord, TArgumentData> mappingRepository,
        IArgumentDataRecorderMappingRegistrator<TParameterRepresentation, TRecord, TArgumentData> mappingRegistrator);
}
