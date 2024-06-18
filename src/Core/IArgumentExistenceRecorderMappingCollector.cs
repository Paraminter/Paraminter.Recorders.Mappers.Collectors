namespace Paraminter.Recorders.Mappers.Collectors;

/// <summary>Collects mappings from parameters to recorders, responsible for recording the existence of arguments of that parameter.</summary>
/// <typeparam name="TParameter">The type of the mapped parameters.</typeparam>
/// <typeparam name="TRecord">The type of the record to which data is recorded.</typeparam>
public interface IArgumentExistenceRecorderMappingCollector<in TParameter, out TRecord>
{
    /// <summary>Adds a mapping from a parameter to a recorder, responsible for recording the existence of arguments of that parameter.</summary>
    /// <param name="parameter">The mapped parameter.</param>
    /// <param name="recorder">The mapped recorder.</param>
    public abstract void AddMapping(
        TParameter parameter,
        IMappedArgumentExistenceRecorder<TRecord> recorder);
}
