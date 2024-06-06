namespace Paraminter.Recorders.Mappers.Collectors;

/// <summary>Handles building of <see cref="IArgumentExistenceRecorderMapper{TParameter, TRecord}"/>.</summary>
/// <typeparam name="TParameter">The type of the mapped parameters.</typeparam>
/// <typeparam name="TRecord">The type of the record to which data is recorded.</typeparam>
public interface IArgumentExistenceRecorderMapperBuilder<in TParameter, in TRecord>
{
    /// <summary>Builds the <see cref="IArgumentExistenceRecorderMapper{TParameter, TRecord}"/>.</summary>
    /// <returns>The built <see cref="IArgumentExistenceRecorderMapper{TParameter, TRecord}"/>.</returns>
    public abstract IArgumentExistenceRecorderMapper<TParameter, TRecord> Build();
}
