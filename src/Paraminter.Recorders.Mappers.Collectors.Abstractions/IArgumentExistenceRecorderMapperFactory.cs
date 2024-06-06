namespace Paraminter.Recorders.Mappers.Collectors;

/// <summary>Handles creation of <see cref="IArgumentExistenceRecorderMapper{TParameter, TRecord}"/> using <see cref="IArgumentExistenceRecorderMappingRepository{TParameter, TParameterRepresentation, TRecord}"/>.</summary>
public interface IArgumentExistenceRecorderMapperFactory
{
    /// <summary>Creates a <see cref="IArgumentExistenceRecorderMapper{TParameter, TRecord}"/>, mapping parameters to recorders.</summary>
    /// <typeparam name="TParameter">The type of the mapped parameters.</typeparam>
    /// <typeparam name="TParameterRepresentation">The type used as a representation of the mapped parameters.</typeparam>
    /// <typeparam name="TRecord">The type of the record to which data is recorded.</typeparam>
    /// <param name="mappingRepository">A repository of mappings from parameters to recorders.</param>
    /// <param name="mappingRegistrator">Registers mappings from parameters to recorders.</param>
    /// <returns>The created <see cref="IArgumentExistenceRecorderMapper{TParameter, TRecord}"/>.</returns>
    public abstract IArgumentExistenceRecorderMapper<TParameter, TRecord> Create<TParameter, TParameterRepresentation, TRecord>(
        IArgumentExistenceRecorderMappingRepository<TParameter, TParameterRepresentation, TRecord> mappingRepository,
        IArgumentExistenceRecorderMappingRegistrator<TParameterRepresentation, TRecord> mappingRegistrator);
}
