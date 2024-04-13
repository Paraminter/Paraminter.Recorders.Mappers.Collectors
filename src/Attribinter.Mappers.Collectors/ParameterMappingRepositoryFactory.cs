namespace Attribinter.Mappers.Collectors;

using Attribinter.Parameters.Representations;

using System;
using System.Collections.Generic;

/// <inheritdoc cref="IParameterMappingRepositoryFactory"/>
public sealed class ParameterMappingRepositoryFactory : IParameterMappingRepositoryFactory
{
    /// <summary>Instantiates a <see cref="ParameterMappingRepositoryFactory"/>, handling creation of <see cref="IParameterMappingRepository{TParameter, TParameterRepresentation, TRecord, TData}"/>.</summary>
    public ParameterMappingRepositoryFactory() { }

    IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TData> IParameterMappingRepositoryFactory.Create<TParameter, TParameterRepresentation, TRecord, TData>(IParameterRepresentationFactory<TParameter, TParameterRepresentation> parameterRepresentationFactory, IEqualityComparer<TParameterRepresentation> parameterRepresentationComparer)
    {
        if (parameterRepresentationFactory is null)
        {
            throw new ArgumentNullException(nameof(parameterRepresentationFactory));
        }

        if (parameterRepresentationComparer is null)
        {
            throw new ArgumentNullException(nameof(parameterRepresentationComparer));
        }

        Dictionary<TParameterRepresentation, IMappedArgumentRecorder<TRecord, TData>> mappings = new(parameterRepresentationComparer);

        ParameterMapper<TParameter, TParameterRepresentation, TRecord, TData> mapper = new(parameterRepresentationFactory, mappings);
        ParameterMappingCollector<TParameter, TParameterRepresentation, TRecord, TData> collector = new(mapper);
        ParameterMapperBuilder<TParameter, TParameterRepresentation, TRecord, TData> builder = new(collector);

        return new ParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TData>(builder, collector);
    }

    private sealed class ParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TData> : IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TData>
    {
        private readonly ParameterMapperBuilder<TParameter, TParameterRepresentation, TRecord, TData> Builder;
        private readonly ParameterMappingCollector<TParameter, TParameterRepresentation, TRecord, TData> Collector;

        public ParameterMappingRepository(ParameterMapperBuilder<TParameter, TParameterRepresentation, TRecord, TData> builder, ParameterMappingCollector<TParameter, TParameterRepresentation, TRecord, TData> collector)
        {
            Builder = builder;
            Collector = collector;
        }

        IParameterMapperBuilder<TParameter, TRecord, TData> IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TData>.Builder => Builder;
        IParameterMappingCollector<TParameterRepresentation, TRecord, TData> IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TData>.Collector => Collector;
    }

    private sealed class ParameterMapperBuilder<TParameter, TParameterRepresentation, TRecord, TData> : IParameterMapperBuilder<TParameter, TRecord, TData>
    {
        private readonly ParameterMappingCollector<TParameter, TParameterRepresentation, TRecord, TData> Collector;

        private bool HasBeenBuilt;

        public ParameterMapperBuilder(ParameterMappingCollector<TParameter, TParameterRepresentation, TRecord, TData> collector)
        {
            Collector = collector;
        }

        IParameterMapper<TParameter, TRecord, TData> IParameterMapperBuilder<TParameter, TRecord, TData>.Build()
        {
            if (HasBeenBuilt)
            {
                throw new InvalidOperationException("Cannot build the mapper, as it has already been built.");
            }

            HasBeenBuilt = true;

            return Collector.FinalizeMapper();
        }
    }

    private sealed class ParameterMappingCollector<TParameter, TParameterRepresentation, TRecord, TData> : IParameterMappingCollector<TParameterRepresentation, TRecord, TData>
    {
        private readonly ParameterMapper<TParameter, TParameterRepresentation, TRecord, TData> Mapper;

        private bool HasBeenFinalized;

        public ParameterMappingCollector(ParameterMapper<TParameter, TParameterRepresentation, TRecord, TData> mapper)
        {
            Mapper = mapper;
        }

        public IParameterMapper<TParameter, TRecord, TData> FinalizeMapper()
        {
            HasBeenFinalized = true;

            return Mapper;
        }

        void IParameterMappingCollector<TParameterRepresentation, TRecord, TData>.AddMapping(TParameterRepresentation parameter, IMappedArgumentRecorder<TRecord, TData> recorder)
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

    private sealed class ParameterMapper<TParameter, TParameterRepresentation, TRecord, TData> : IParameterMapper<TParameter, TRecord, TData>
    {
        private readonly IParameterRepresentationFactory<TParameter, TParameterRepresentation> ParameterRepresentationFactory;
        private readonly IDictionary<TParameterRepresentation, IMappedArgumentRecorder<TRecord, TData>> Mappings;

        public ParameterMapper(IParameterRepresentationFactory<TParameter, TParameterRepresentation> parameterRepresentationFactory, IDictionary<TParameterRepresentation, IMappedArgumentRecorder<TRecord, TData>> mappings)
        {
            ParameterRepresentationFactory = parameterRepresentationFactory;
            Mappings = mappings;
        }

        public void AddMapping(TParameterRepresentation parameter, IMappedArgumentRecorder<TRecord, TData> recorder) => Mappings.Add(parameter, recorder);

        IMappedArgumentRecorder<TRecord, TData>? IParameterMapper<TParameter, TRecord, TData>.TryMapParameter(TParameter parameter)
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
