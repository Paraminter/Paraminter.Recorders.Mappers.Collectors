namespace Paraminter.Recorders.Mappers.Collectors;

using System;

/// <inheritdoc cref="IArgumentExistenceRecorderMapperFactory"/>
public sealed class ArgumentExistenceRecorderMapperFactory
    : IArgumentExistenceRecorderMapperFactory
{
    /// <summary>Instantiates a <see cref="ArgumentExistenceRecorderMapperFactory"/>, handling creation of <see cref="IArgumentExistenceRecorderMapper{TParameter, TRecord}"/>.</summary>
    public ArgumentExistenceRecorderMapperFactory() { }

    IArgumentExistenceRecorderMapper<TParameter, TRecord> IArgumentExistenceRecorderMapperFactory.Create<TParameter, TParameterRepresentation, TRecord>(
        IArgumentExistenceRecorderMappingRepository<TParameter, TParameterRepresentation, TRecord> mappingRepository,
        IArgumentExistenceRecorderMappingRegistrator<TParameterRepresentation, TRecord> mappingRegistrator)
    {
        if (mappingRepository is null)
        {
            throw new ArgumentNullException(nameof(mappingRepository));
        }

        if (mappingRegistrator is null)
        {
            throw new ArgumentNullException(nameof(mappingRegistrator));
        }

        mappingRegistrator.Register(mappingRepository.Collector);

        return mappingRepository.Builder.Build();
    }
}
