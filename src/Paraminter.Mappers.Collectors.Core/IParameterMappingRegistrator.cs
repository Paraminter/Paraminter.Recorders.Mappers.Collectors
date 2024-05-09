namespace Paraminter.Mappers.Collectors;

/// <summary>Registers mappings from parameters to recorders with <see cref="IParameterMappingCollector{TParameter, TRecord, TArgumentData}"/>.</summary>
/// <typeparam name="TParameter">The type of the mapped parameters.</typeparam>
/// <typeparam name="TRecord">The type of the record to which data is recorded.</typeparam>
/// <typeparam name="TArgumentData">The type representing data about the arguments.</typeparam>
public interface IParameterMappingRegistrator<out TParameter, in TRecord, in TArgumentData>
{
    /// <summary>Registers mappings from parameters to recorders with a <see cref="IParameterMappingCollector{TParameter, TRecord, TArgumentData}"/>.</summary>
    /// <param name="collector">Collects mappings from parameters to recorders.</param>
    public abstract void Register(IParameterMappingCollector<TParameter, TRecord, TArgumentData> collector);
}
