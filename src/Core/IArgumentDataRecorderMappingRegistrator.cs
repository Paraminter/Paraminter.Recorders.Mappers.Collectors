namespace Paraminter.Recorders.Mappers.Collectors;

/// <summary>Registers mappings from parameters to recorders with <see cref="IArgumentDataRecorderMappingCollector{TParameter, TRecord, TArgumentData}"/>.</summary>
/// <typeparam name="TParameter">The type of the mapped parameters.</typeparam>
/// <typeparam name="TRecord">The type of the record to which data is recorded.</typeparam>
/// <typeparam name="TArgumentData">The type representing data about the arguments.</typeparam>
public interface IArgumentDataRecorderMappingRegistrator<out TParameter, in TRecord, in TArgumentData>
{
    /// <summary>Registers mappings from parameters to recorders with a <see cref="IArgumentDataRecorderMappingCollector{TParameter, TRecord, TArgumentData}"/>.</summary>
    /// <param name="collector">Collects mappings from parameters to recorders.</param>
    public abstract void Register(
        IArgumentDataRecorderMappingCollector<TParameter, TRecord, TArgumentData> collector);
}
