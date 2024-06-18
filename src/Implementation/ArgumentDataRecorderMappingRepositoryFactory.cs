namespace Paraminter.Recorders.Mappers.Collectors;

using Paraminter.Parameters.Representations;

using System;
using System.Collections.Generic;

/// <inheritdoc cref="IArgumentDataRecorderMappingRepositoryFactory"/>
public sealed class ArgumentDataRecorderMappingRepositoryFactory
    : IArgumentDataRecorderMappingRepositoryFactory
{
    /// <summary>Instantiates a <see cref="ArgumentDataRecorderMappingRepositoryFactory"/>, handling creation of <see cref="IArgumentDataRecorderMappingRepository{TParameter, TParameterRepresentation, TRecord, TArgumentData}"/>.</summary>
    public ArgumentDataRecorderMappingRepositoryFactory() { }

    IArgumentDataRecorderMappingRepository<TParameter, TParameterRepresentation, TRecord, TArgumentData> IArgumentDataRecorderMappingRepositoryFactory.Create<TParameter, TParameterRepresentation, TRecord, TArgumentData>(
        IParameterRepresentationFactory<TParameter, TParameterRepresentation> parameterRepresentationFactory,
        IEqualityComparer<TParameterRepresentation> parameterRepresentationComparer)
    {
        if (parameterRepresentationFactory is null)
        {
            throw new ArgumentNullException(nameof(parameterRepresentationFactory));
        }

        if (parameterRepresentationComparer is null)
        {
            throw new ArgumentNullException(nameof(parameterRepresentationComparer));
        }

        Dictionary<TParameterRepresentation, IMappedArgumentDataRecorder<TRecord, TArgumentData>> mappings = new(parameterRepresentationComparer);

        ArgumentDataRecorderMapper<TParameter, TParameterRepresentation, TRecord, TArgumentData> mapper = new(parameterRepresentationFactory, mappings);
        ArgumentDataRecorderMappingCollector<TParameter, TParameterRepresentation, TRecord, TArgumentData> collector = new(mapper);
        ArgumentDataRecorderMapperBuilder<TParameter, TParameterRepresentation, TRecord, TArgumentData> builder = new(collector);

        return new ArgumentDataRecorderMappingRepository<TParameter, TParameterRepresentation, TRecord, TArgumentData>(builder, collector);
    }

    private sealed class ArgumentDataRecorderMappingRepository<TParameter, TParameterRepresentation, TRecord, TArgumentData>
        : IArgumentDataRecorderMappingRepository<TParameter, TParameterRepresentation, TRecord, TArgumentData>
    {
        private readonly ArgumentDataRecorderMapperBuilder<TParameter, TParameterRepresentation, TRecord, TArgumentData> Builder;
        private readonly ArgumentDataRecorderMappingCollector<TParameter, TParameterRepresentation, TRecord, TArgumentData> Collector;

        public ArgumentDataRecorderMappingRepository(
            ArgumentDataRecorderMapperBuilder<TParameter, TParameterRepresentation, TRecord, TArgumentData> builder,
            ArgumentDataRecorderMappingCollector<TParameter, TParameterRepresentation, TRecord, TArgumentData> collector)
        {
            Builder = builder;
            Collector = collector;
        }

        IArgumentDataRecorderMapperBuilder<TParameter, TRecord, TArgumentData> IArgumentDataRecorderMappingRepository<TParameter, TParameterRepresentation, TRecord, TArgumentData>.Builder => Builder;
        IArgumentDataRecorderMappingCollector<TParameterRepresentation, TRecord, TArgumentData> IArgumentDataRecorderMappingRepository<TParameter, TParameterRepresentation, TRecord, TArgumentData>.Collector => Collector;
    }

    private sealed class ArgumentDataRecorderMapperBuilder<TParameter, TParameterRepresentation, TRecord, TArgumentData>
        : IArgumentDataRecorderMapperBuilder<TParameter, TRecord, TArgumentData>
    {
        private readonly ArgumentDataRecorderMappingCollector<TParameter, TParameterRepresentation, TRecord, TArgumentData> Collector;

        private bool HasBeenBuilt;

        public ArgumentDataRecorderMapperBuilder(
            ArgumentDataRecorderMappingCollector<TParameter, TParameterRepresentation, TRecord, TArgumentData> collector)
        {
            Collector = collector;
        }

        IArgumentDataRecorderMapper<TParameter, TRecord, TArgumentData> IArgumentDataRecorderMapperBuilder<TParameter, TRecord, TArgumentData>.Build()
        {
            if (HasBeenBuilt)
            {
                throw new InvalidOperationException("Cannot build the mapper, as it has already been built.");
            }

            HasBeenBuilt = true;

            return Collector.FinalizeMapper();
        }
    }

    private sealed class ArgumentDataRecorderMappingCollector<TParameter, TParameterRepresentation, TRecord, TArgumentData>
        : IArgumentDataRecorderMappingCollector<TParameterRepresentation, TRecord, TArgumentData>
    {
        private readonly ArgumentDataRecorderMapper<TParameter, TParameterRepresentation, TRecord, TArgumentData> Mapper;

        private bool HasBeenFinalized;

        public ArgumentDataRecorderMappingCollector(
            ArgumentDataRecorderMapper<TParameter, TParameterRepresentation, TRecord, TArgumentData> mapper)
        {
            Mapper = mapper;
        }

        public IArgumentDataRecorderMapper<TParameter, TRecord, TArgumentData> FinalizeMapper()
        {
            HasBeenFinalized = true;

            return Mapper;
        }

        void IArgumentDataRecorderMappingCollector<TParameterRepresentation, TRecord, TArgumentData>.AddMapping(
            TParameterRepresentation parameter,
            IMappedArgumentDataRecorder<TRecord, TArgumentData> recorder)
        {
            if (parameter is null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            if (recorder is null)
            {
                throw new ArgumentNullException(nameof(recorder));
            }

            if (HasBeenFinalized)
            {
                throw new InvalidOperationException("Cannot add mapping, as the mapper has already been finalized.");
            }

            Mapper.AddMapping(parameter, recorder);
        }
    }

    private sealed class ArgumentDataRecorderMapper<TParameter, TParameterRepresentation, TRecord, TArgumentData>
        : IArgumentDataRecorderMapper<TParameter, TRecord, TArgumentData>
    {
        private readonly IParameterRepresentationFactory<TParameter, TParameterRepresentation> ParameterRepresentationFactory;
        private readonly IDictionary<TParameterRepresentation, IMappedArgumentDataRecorder<TRecord, TArgumentData>> Mappings;

        public ArgumentDataRecorderMapper(
            IParameterRepresentationFactory<TParameter, TParameterRepresentation> parameterRepresentationFactory,
            IDictionary<TParameterRepresentation, IMappedArgumentDataRecorder<TRecord, TArgumentData>> mappings)
        {
            ParameterRepresentationFactory = parameterRepresentationFactory;
            Mappings = mappings;
        }

        public void AddMapping(
            TParameterRepresentation parameter,
            IMappedArgumentDataRecorder<TRecord, TArgumentData> recorder)
        {
            Mappings.Add(parameter, recorder);
        }

        IMappedArgumentDataRecorder<TRecord, TArgumentData>? IArgumentDataRecorderMapper<TParameter, TRecord, TArgumentData>.TryMapParameter(
            TParameter parameter)
        {
            if (parameter is null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            var parameterRepresentation = ParameterRepresentationFactory.Create(parameter);

            if (Mappings.TryGetValue(parameterRepresentation, out var recorder) is false)
            {
                return null;
            }

            return recorder;
        }
    }
}
