namespace Paraminter.Mappers.Collectors;

using System;

/// <inheritdoc cref="IParameterMapperFactory"/>
public sealed class ParameterMapperFactory : IParameterMapperFactory
{
    /// <summary>Instantiates a <see cref="ParameterMapperFactory"/>, handling creation of <see cref="IParameterMapper{TParameter, TRecord, TArgumentData}"/>.</summary>
    public ParameterMapperFactory() { }

    IParameterMapper<TParameter, TRecord, TArgumentData> IParameterMapperFactory.Create<TParameter, TParameterRepresentation, TRecord, TArgumentData>(IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TArgumentData> parameterMappingRepository, IParameterMappingRegistrator<TParameterRepresentation, TRecord, TArgumentData> parameterMappingRegistrator)
    {
        if (parameterMappingRepository is null)
        {
            throw new ArgumentNullException(nameof(parameterMappingRepository));
        }

        if (parameterMappingRegistrator is null)
        {
            throw new ArgumentNullException(nameof(parameterMappingRegistrator));
        }

        parameterMappingRegistrator.Register(parameterMappingRepository.Collector);

        return parameterMappingRepository.Builder.Build();
    }
}
