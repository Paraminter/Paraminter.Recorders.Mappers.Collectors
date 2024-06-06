namespace Paraminter.Recorders.Mappers.Collectors;

/// <summary>Registers mappings from parameters to recorders with <see cref="IArgumentExistenceRecorderMappingCollector{TParameter, TRecord}"/>.</summary>
/// <typeparam name="TParameter">The type of the mapped parameters.</typeparam>
/// <typeparam name="TRecord">The type of the record to which data is recorded.</typeparam>
public interface IArgumentExistenceRecorderMappingRegistrator<out TParameter, in TRecord>
{
    /// <summary>Registers mappings from parameters to recorders with a <see cref="IArgumentExistenceRecorderMappingCollector{TParameter, TRecord}"/>.</summary>
    /// <param name="collector">Collects mappings from parameters to recorders.</param>
    public abstract void Register(
        IArgumentExistenceRecorderMappingCollector<TParameter, TRecord> collector);
}
