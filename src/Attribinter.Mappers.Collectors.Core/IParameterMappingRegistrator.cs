namespace Attribinter.Mappers.Collectors;

/// <summary>Registers mappings from parameters to recorders with <see cref="IParameterMappingCollector{TParameter, TRecord, TData}"/>.</summary>
/// <typeparam name="TParameter">The type of the mapped parameters.</typeparam>
/// <typeparam name="TRecord">The type of the data record to which the mapped recorders record data.</typeparam>
/// <typeparam name="TData">The type of the data recorded by the mapped recorders.</typeparam>
public interface IParameterMappingRegistrator<out TParameter, in TRecord, in TData>
{
    /// <summary>Registers mappings from parameters to recorders with a <see cref="IParameterMappingCollector{TParameter, TRecord, TData}"/>.</summary>
    /// <param name="collector">Collects mappings from parameters to recorders.</param>
    public abstract void Register(IParameterMappingCollector<TParameter, TRecord, TData> collector);
}
