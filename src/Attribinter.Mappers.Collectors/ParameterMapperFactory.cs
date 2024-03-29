namespace Attribinter.Mappers.Collectors;

using System;

/// <inheritdoc cref="IParameterMapperFactory"/>
public sealed class ParameterMapperFactory : IParameterMapperFactory
{
    /// <summary>Instantiates a <see cref="ParameterMapperFactory"/>, handling creation of <see cref="IParameterMapper{TParameter, TRecord, TData}"/>.</summary>
    public ParameterMapperFactory() { }

    IParameterMapper<TParameter, TRecord, TData> IParameterMapperFactory.Create<TParameter, TParameterRepresentation, TRecord, TData>(IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TData> parameterMappingRepository, IParameterMappingRegistrator<TParameterRepresentation, TRecord, TData> parameterMappingRegistrator)
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
