namespace Paraminter.Mappers.Collectors;

/// <summary>Handles building of <see cref="IParameterMapper{TParameter, TRecord, TArgumentData}"/>.</summary>
/// <typeparam name="TParameter">The type of the mapped parameters.</typeparam>
/// <typeparam name="TRecord">The type of the record to which data is recorded.</typeparam>
/// <typeparam name="TArgumentData">The type representing data about the arguments.</typeparam>
public interface IParameterMapperBuilder<in TParameter, in TRecord, in TArgumentData>
{
    /// <summary>Builds the <see cref="IParameterMapper{TParameter, TRecord, TArgumentData}"/>.</summary>
    /// <returns>The built <see cref="IParameterMapper{TParameter, TRecord, TArgumentData}"/>.</returns>
    public abstract IParameterMapper<TParameter, TRecord, TArgumentData> Build();
}
