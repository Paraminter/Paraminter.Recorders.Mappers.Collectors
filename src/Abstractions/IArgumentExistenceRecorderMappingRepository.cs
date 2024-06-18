namespace Paraminter.Recorders.Mappers.Collectors;

/// <summary>A repository of mappings from parameters to recorder, responsible for recording the existence of arguments of that parameter.</summary>
/// <typeparam name="TParameter">The type of the mapped parameters.</typeparam>
/// <typeparam name="TParameterRepresentation">The type used as a representation of the mapped parameters.</typeparam>
/// <typeparam name="TRecord">The type of the record to which data is recorded.</typeparam>
public interface IArgumentExistenceRecorderMappingRepository<TParameter, TParameterRepresentation, TRecord>
{
    /// <summary>Handles building of <see cref="IArgumentExistenceRecorderMapper{TParameter, TRecord}"/>.</summary>
    public abstract IArgumentExistenceRecorderMapperBuilder<TParameter, TRecord> Builder { get; }

    /// <summary>Collects mappings from parameters to recorders.</summary>
    public abstract IArgumentExistenceRecorderMappingCollector<TParameterRepresentation, TRecord> Collector { get; }
}
