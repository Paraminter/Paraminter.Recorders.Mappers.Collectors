namespace Paraminter.Mappers.Collectors;

/// <summary>A repository of mappings from parameters to recorder, responsible for recording data about the arguments of that parameter.</summary>
/// <typeparam name="TParameter">The type of the mapped parameters.</typeparam>
/// <typeparam name="TParameterRepresentation">The type used as a representation of the mapped parameters.</typeparam>
/// <typeparam name="TRecord">The type of the record to which data is recorded.</typeparam>
/// <typeparam name="TArgumentData">The type representing data about the arguments.</typeparam>
public interface IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TArgumentData>
{
    /// <summary>Handles building of <see cref="IParameterMapper{TParameter, TRecord, TArgumentData}"/>.</summary>
    public abstract IParameterMapperBuilder<TParameter, TRecord, TArgumentData> Builder { get; }

    /// <summary>Collects mappings from parameters to recorders.</summary>
    public abstract IParameterMappingCollector<TParameterRepresentation, TRecord, TArgumentData> Collector { get; }
}
