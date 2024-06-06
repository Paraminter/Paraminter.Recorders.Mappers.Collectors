namespace Paraminter.Recorders.Mappers.Collectors;

using System;

/// <inheritdoc cref="IArgumentDataRecorderMapperFactory"/>
public sealed class ArgumentDataRecorderMapperFactory
    : IArgumentDataRecorderMapperFactory
{
    /// <summary>Instantiates a <see cref="ArgumentDataRecorderMapperFactory"/>, handling creation of <see cref="IArgumentDataRecorderMapper{TParameter, TRecord, TArgumentData}"/>.</summary>
    public ArgumentDataRecorderMapperFactory() { }

    IArgumentDataRecorderMapper<TParameter, TRecord, TArgumentData> IArgumentDataRecorderMapperFactory.Create<TParameter, TParameterRepresentation, TRecord, TArgumentData>(
        IArgumentDataRecorderMappingRepository<TParameter, TParameterRepresentation, TRecord, TArgumentData> mappingRepository,
        IArgumentDataRecorderMappingRegistrator<TParameterRepresentation, TRecord, TArgumentData> mappingRegistrator)
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
