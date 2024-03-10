namespace Attribinter.Mappers.Collectors;

using System;
using System.Collections.Generic;

/// <inheritdoc cref="IParameterMappingRepositoryFactory"/>
public sealed class ParameterMappingRepositoryFactory : IParameterMappingRepositoryFactory
{
    /// <summary>Instantiates a <see cref="ParameterMappingRepositoryFactory"/>, handling creation of <see cref="IParameterMappingRepository{TParameter, TRecord, TData}"/>.</summary>
    public ParameterMappingRepositoryFactory() { }

    IParameterMappingRepositoryFactory<TParameter> IParameterMappingRepositoryFactory.ForParameter<TParameter>(IEqualityComparer<TParameter> parameterComparer)
    {
        if (parameterComparer is null)
        {
            throw new ArgumentNullException(nameof(parameterComparer));
        }

        return new GenericParameterMappingRepositoryFactory<TParameter>(parameterComparer);
    }

    private sealed class GenericParameterMappingRepositoryFactory<TParameter> : IParameterMappingRepositoryFactory<TParameter>
    {
        private readonly IEqualityComparer<TParameter> ParameterComparer;

        public GenericParameterMappingRepositoryFactory(IEqualityComparer<TParameter> parameterComparer)
        {
            ParameterComparer = parameterComparer;
        }

        IParameterMappingRepository<TParameter, TRecord, TData> IParameterMappingRepositoryFactory<TParameter>.Create<TRecord, TData>()
        {
            Dictionary<TParameter, IMappedArgumentRecorder<TRecord, TData>> mappings = new(ParameterComparer);

            ParameterMapper<TRecord, TData> mapper = new(mappings);
            ParameterMappingCollector<TRecord, TData> collector = new(mapper);
            ParameterMapperBuilder<TRecord, TData> builder = new(collector);

            return new ParameterMappingRepository<TRecord, TData>(builder, collector);
        }

        private sealed class ParameterMappingRepository<TRecord, TData> : IParameterMappingRepository<TParameter, TRecord, TData>
        {
            private readonly ParameterMapperBuilder<TRecord, TData> Builder;
            private readonly ParameterMappingCollector<TRecord, TData> Collector;

            public ParameterMappingRepository(ParameterMapperBuilder<TRecord, TData> builder, ParameterMappingCollector<TRecord, TData> collector)
            {
                Builder = builder;
                Collector = collector;
            }

            IParameterMapperBuilder<TParameter, TRecord, TData> IParameterMappingRepository<TParameter, TRecord, TData>.Builder => Builder;
            IParameterMappingCollector<TParameter, TRecord, TData> IParameterMappingRepository<TParameter, TRecord, TData>.Collector => Collector;
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

        private sealed class ParameterMappingCollector<TRecord, TData> : IParameterMappingCollector<TParameter, TRecord, TData>
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

            void IParameterMappingCollector<TParameter, TRecord, TData>.AddMapping(TParameter parameter, IMappedArgumentRecorder<TRecord, TData> recorder)
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
            private readonly IDictionary<TParameter, IMappedArgumentRecorder<TRecord, TData>> Mappings;

            public ParameterMapper(IDictionary<TParameter, IMappedArgumentRecorder<TRecord, TData>> mappings)
            {
                Mappings = mappings;
            }

            public void AddMapping(TParameter parameter, IMappedArgumentRecorder<TRecord, TData> recorder) => Mappings.Add(parameter, recorder);

            IMappedArgumentRecorder<TRecord, TData>? IParameterMapper<TParameter, TRecord, TData>.TryMapParameter(TParameter parameter)
            {
                if (parameter is null)
                {
                    throw new ArgumentNullException(nameof(parameter));
                }

                if (Mappings.TryGetValue(parameter, out var recorder) is false)
                {
                    return null;
                }

                return recorder;
            }
        }
    }
}
