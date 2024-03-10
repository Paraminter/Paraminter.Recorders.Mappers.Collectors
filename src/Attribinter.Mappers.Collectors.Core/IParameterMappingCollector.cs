namespace Attribinter.Mappers.Collectors;

/// <summary>Collects mappings from parameters to recorders, responsible for recording data about the arguments of that parameter.</summary>
/// <typeparam name="TParameter">The type of the mapped parameters.</typeparam>
/// <typeparam name="TRecord">The type of the data record to which the mapped recorders record data.</typeparam>
/// <typeparam name="TData">The type of the data recorded by the mapped recorders.</typeparam>
public interface IParameterMappingCollector<in TParameter, out TRecord, out TData>
{
    /// <summary>Adds a mapping from a parameter to a recorder, responsible for recording data about the arguments of that parameter.</summary>
    /// <param name="parameter">The mapped parameter.</param>
    /// <param name="recorder">The mapped recorder.</param>
    public abstract void AddMapping(TParameter parameter, IMappedArgumentRecorder<TRecord, TData> recorder);
}
