namespace Attribinter.Mappers.Collectors;

/// <summary>Handles building of <see cref="IParameterMapper{TParameter, TRecord, TData}"/>.</summary>
/// <typeparam name="TParameter">The type of the mapped parameters.</typeparam>
/// <typeparam name="TRecord">The type of the data record to which the mapped recorders record data.</typeparam>
/// <typeparam name="TData">The type of the data recorded by the mapped recorders.</typeparam>
public interface IParameterMapperBuilder<in TParameter, in TRecord, in TData>
{
    /// <summary>Builds the <see cref="IParameterMapper{TParameter, TRecord, TData}"/>.</summary>
    /// <returns>The built <see cref="IParameterMapper{TParameter, TRecord, TData}"/>.</returns>
    public abstract IParameterMapper<TParameter, TRecord, TData> Build();
}
