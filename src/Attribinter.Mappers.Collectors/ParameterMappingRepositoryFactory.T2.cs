namespace Attribinter.Mappers.Collectors;

using Attribinter.Parameters.Representations;

using System;
using System.Collections.Generic;

/// <inheritdoc cref="IParameterMappingRepositoryFactory{TParameter, TParameterRepresentation}"/>
public sealed class ParameterMappingRepositoryFactory<TParameter, TParameterRepresentation> : IParameterMappingRepositoryFactory<TParameter, TParameterRepresentation>
{
    private readonly IParameterRepresentationFactory<TParameter, TParameterRepresentation> ParameterRepresentationFactory;

    /// <summary>Instantiates a <see cref="ParameterMappingRepositoryFactory{TParameter, TParameterRepresentation}"/>, handling creation of <see cref="IParameterMappingRepository{TParameter, TParameterRepresentation, TRecord, TData}"/>.</summary>
    /// <param name="parameterRepresentationFactory">Handles creation of parameter representations.</param>
    public ParameterMappingRepositoryFactory(IParameterRepresentationFactory<TParameter, TParameterRepresentation> parameterRepresentationFactory)
    {
        ParameterRepresentationFactory = parameterRepresentationFactory ?? throw new ArgumentNullException(nameof(parameterRepresentationFactory));
    }

    IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TData> IParameterMappingRepositoryFactory<TParameter, TParameterRepresentation>.Create<TRecord, TData>(IEqualityComparer<TParameterRepresentation> parameterRepresentationComparer)
    {
        if (parameterRepresentationComparer is null)
        {
            throw new ArgumentNullException(nameof(parameterRepresentationComparer));
        }

        Dictionary<TParameterRepresentation, IMappedArgumentRecorder<TRecord, TData>> mappings = new(parameterRepresentationComparer);

        ParameterMapper<TRecord, TData> mapper = new(ParameterRepresentationFactory, mappings);
        ParameterMappingCollector<TRecord, TData> collector = new(mapper);
        ParameterMapperBuilder<TRecord, TData> builder = new(collector);

        return new ParameterMappingRepository<TRecord, TData>(builder, collector);
    }

    private sealed class ParameterMappingRepository<TRecord, TData> : IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TData>
    {
        private readonly ParameterMapperBuilder<TRecord, TData> Builder;
        private readonly ParameterMappingCollector<TRecord, TData> Collector;

        public ParameterMappingRepository(ParameterMapperBuilder<TRecord, TData> builder, ParameterMappingCollector<TRecord, TData> collector)
        {
            Builder = builder;
            Collector = collector;
        }

        IParameterMapperBuilder<TParameter, TRecord, TData> IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TData>.Builder => Builder;
        IParameterMappingCollector<TParameterRepresentation, TRecord, TData> IParameterMappingRepository<TParameter, TParameterRepresentation, TRecord, TData>.Collector => Collector;
    }

    private sealed class ParameterMapperBuilder<TRecord, TData> : IParameterMapperBuilder<TParameter, TRecord, TData>
    {
        private readonly ParameterMappingCollector<TRecord, TData> Collector;

        private bool HasBeenBuilt;

        public ParameterMapperBuilder(ParameterMappingCollector<TRecord, TData> collector)
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

    private sealed class ParameterMappingCollector<TRecord, TData> : IParameterMappingCollector<TParameterRepresentation, TRecord, TData>
    {
        private readonly ParameterMapper<TRecord, TData> Mapper;

        private bool HasBeenFinalized;

        public ParameterMappingCollector(ParameterMapper<TRecord, TData> mapper)
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

    private sealed class ParameterMapper<TRecord, TData> : IParameterMapper<TParameter, TRecord, TData>
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
