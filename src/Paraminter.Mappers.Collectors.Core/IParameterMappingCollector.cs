namespace Paraminter.Mappers.Collectors;

/// <summary>Collects mappings from parameters to recorders, responsible for recording data about the arguments of that parameter.</summary>
/// <typeparam name="TParameter">The type of the mapped parameters.</typeparam>
/// <typeparam name="TRecord">The type of the record to which data is recorded.</typeparam>
/// <typeparam name="TArgumentData">The type representing data about the arguments.</typeparam>
public interface IParameterMappingCollector<in TParameter, out TRecord, out TArgumentData>
{
    /// <summary>Adds a mapping from a parameter to a recorder, responsible for recording data about the arguments of that parameter.</summary>
    /// <param name="parameter">The mapped parameter.</param>
    /// <param name="recorder">The mapped recorder.</param>
    public abstract void AddMapping(TParameter parameter, IMappedArgumentDataRecorder<TRecord, TArgumentData> recorder);
}
