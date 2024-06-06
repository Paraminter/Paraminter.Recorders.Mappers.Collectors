namespace Paraminter.Recorders.Mappers.Collectors;

/// <summary>Handles building of <see cref="IArgumentDataRecorderMapper{TParameter, TRecord, TArgumentData}"/>.</summary>
/// <typeparam name="TParameter">The type of the mapped parameters.</typeparam>
/// <typeparam name="TRecord">The type of the record to which data is recorded.</typeparam>
/// <typeparam name="TArgumentData">The type representing data about the arguments.</typeparam>
public interface IArgumentDataRecorderMapperBuilder<in TParameter, in TRecord, in TArgumentData>
{
    /// <summary>Builds the <see cref="IArgumentDataRecorderMapper{TParameter, TRecord, TArgumentData}"/>.</summary>
    /// <returns>The built <see cref="IArgumentDataRecorderMapper{TParameter, TRecord, TArgumentData}"/>.</returns>
    public abstract IArgumentDataRecorderMapper<TParameter, TRecord, TArgumentData> Build();
}
