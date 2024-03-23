namespace Attribinter.Mappers.Collectors;

/// <summary>A repository of mappings from parameters to recorder, responsible for recording data about the arguments of that parameter.</summary>
/// <typeparam name="TParameter">The type of the mapped parameters.</typeparam>
/// <typeparam name="TParameterRepresentation">The type used as a representation of the mapped parameters.</typeparam>
/// <typeparam name="TRecord">The type of the data record to which the mapped recorders record data.</typeparam>
/// <typeparam name="TData">The type of the data recorded by the mapped recorders.</typeparam>
public interface IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TData>
{
    /// <summary>Handles building of <see cref="IParameterMapper{TParameter, TRecord, TData}"/>.</summary>
    public abstract IParameterMapperBuilder<TParameter, TRecord, TData> Builder { get; }

    /// <summary>Collects mappings from parameters to recorders.</summary>
    public abstract IParameterMappingCollector<TParameterRepresentation, TRecord, TData> Collector { get; }
}
