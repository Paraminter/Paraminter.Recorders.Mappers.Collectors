namespace Paraminter.Recorders.Mappers.Collectors;

using Paraminter.Parameters.Representations;

using System;
using System.Collections.Generic;

/// <inheritdoc cref="IArgumentExistenceRecorderMappingRepositoryFactory"/>
public sealed class ArgumentExistenceRecorderMappingRepositoryFactory
    : IArgumentExistenceRecorderMappingRepositoryFactory
{
    /// <summary>Instantiates a <see cref="ArgumentExistenceRecorderMappingRepositoryFactory"/>, handling creation of <see cref="IArgumentExistenceRecorderMappingRepository{TParameter, TParameterRepresentation, TRecord}"/>.</summary>
    public ArgumentExistenceRecorderMappingRepositoryFactory() { }

    IArgumentExistenceRecorderMappingRepository<TParameter, TParameterRepresentation, TRecord> IArgumentExistenceRecorderMappingRepositoryFactory.Create<TParameter, TParameterRepresentation, TRecord>(
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

        Dictionary<TParameterRepresentation, IMappedArgumentExistenceRecorder<TRecord>> mappings = new(parameterRepresentationComparer);

        ArgumentExistenceRecorderMapper<TParameter, TParameterRepresentation, TRecord> mapper = new(parameterRepresentationFactory, mappings);
        ArgumentExistenceRecorderMappingCollector<TParameter, TParameterRepresentation, TRecord> collector = new(mapper);
        ArgumentExistenceRecorderMapperBuilder<TParameter, TParameterRepresentation, TRecord> builder = new(collector);

        return new ArgumentExistenceRecorderMappingRepository<TParameter, TParameterRepresentation, TRecord>(builder, collector);
    }

    private sealed class ArgumentExistenceRecorderMappingRepository<TParameter, TParameterRepresentation, TRecord>
        : IArgumentExistenceRecorderMappingRepository<TParameter, TParameterRepresentation, TRecord>
    {
        private readonly ArgumentExistenceRecorderMapperBuilder<TParameter, TParameterRepresentation, TRecord> Builder;
        private readonly ArgumentExistenceRecorderMappingCollector<TParameter, TParameterRepresentation, TRecord> Collector;

        public ArgumentExistenceRecorderMappingRepository(
            ArgumentExistenceRecorderMapperBuilder<TParameter, TParameterRepresentation, TRecord> builder,
            ArgumentExistenceRecorderMappingCollector<TParameter, TParameterRepresentation, TRecord> collector)
        {
            Builder = builder;
            Collector = collector;
        }

        IArgumentExistenceRecorderMapperBuilder<TParameter, TRecord> IArgumentExistenceRecorderMappingRepository<TParameter, TParameterRepresentation, TRecord>.Builder => Builder;
        IArgumentExistenceRecorderMappingCollector<TParameterRepresentation, TRecord> IArgumentExistenceRecorderMappingRepository<TParameter, TParameterRepresentation, TRecord>.Collector => Collector;
    }

    private sealed class ArgumentExistenceRecorderMapperBuilder<TParameter, TParameterRepresentation, TRecord>
        : IArgumentExistenceRecorderMapperBuilder<TParameter, TRecord>
    {
        private readonly ArgumentExistenceRecorderMappingCollector<TParameter, TParameterRepresentation, TRecord> Collector;

        private bool HasBeenBuilt;

        public ArgumentExistenceRecorderMapperBuilder(
            ArgumentExistenceRecorderMappingCollector<TParameter, TParameterRepresentation, TRecord> collector)
        {
            Collector = collector;
        }

        IArgumentExistenceRecorderMapper<TParameter, TRecord> IArgumentExistenceRecorderMapperBuilder<TParameter, TRecord>.Build()
        {
            if (HasBeenBuilt)
            {
                throw new InvalidOperationException("Cannot build the mapper, as it has already been built.");
            }

            HasBeenBuilt = true;

            return Collector.FinalizeMapper();
        }
    }

    private sealed class ArgumentExistenceRecorderMappingCollector<TParameter, TParameterRepresentation, TRecord>
        : IArgumentExistenceRecorderMappingCollector<TParameterRepresentation, TRecord>
    {
        private readonly ArgumentExistenceRecorderMapper<TParameter, TParameterRepresentation, TRecord> Mapper;

        private bool HasBeenFinalized;

        public ArgumentExistenceRecorderMappingCollector(
            ArgumentExistenceRecorderMapper<TParameter, TParameterRepresentation, TRecord> mapper)
        {
            Mapper = mapper;
        }

        public IArgumentExistenceRecorderMapper<TParameter, TRecord> FinalizeMapper()
        {
            HasBeenFinalized = true;

            return Mapper;
        }

        void IArgumentExistenceRecorderMappingCollector<TParameterRepresentation, TRecord>.AddMapping(
            TParameterRepresentation parameter,
            IMappedArgumentExistenceRecorder<TRecord> recorder)
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

    private sealed class ArgumentExistenceRecorderMapper<TParameter, TParameterRepresentation, TRecord>
        : IArgumentExistenceRecorderMapper<TParameter, TRecord>
    {
        private readonly IParameterRepresentationFactory<TParameter, TParameterRepresentation> ParameterRepresentationFactory;
        private readonly IDictionary<TParameterRepresentation, IMappedArgumentExistenceRecorder<TRecord>> Mappings;

        public ArgumentExistenceRecorderMapper(
            IParameterRepresentationFactory<TParameter, TParameterRepresentation> parameterRepresentationFactory,
            IDictionary<TParameterRepresentation, IMappedArgumentExistenceRecorder<TRecord>> mappings)
        {
            ParameterRepresentationFactory = parameterRepresentationFactory;
            Mappings = mappings;
        }

        public void AddMapping(
            TParameterRepresentation parameter,
            IMappedArgumentExistenceRecorder<TRecord> recorder)
        {
            Mappings.Add(parameter, recorder);
        }

        IMappedArgumentExistenceRecorder<TRecord>? IArgumentExistenceRecorderMapper<TParameter, TRecord>.TryMapParameter(
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
