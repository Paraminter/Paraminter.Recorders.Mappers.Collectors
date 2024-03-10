namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.GenericParameterMappingRepositoryFactoryCases.ParameterMapperCases;

using Moq;

using System;
using System.Collections.Generic;

internal sealed class MapperContext<TParameter, TRecord, TData>
{
    public static MapperContext<TParameter, TRecord, TData> Create(Action<Mock<IEqualityComparer<TParameter>>> parameterComparerMockSetup, Action<IParameterMappingCollector<TParameter, TRecord, TData>> registrator)
    {
        ParameterMappingRepositoryFactory nonGenericFactory = new();

        Mock<IEqualityComparer<TParameter>> parameterComparerMock = new();

        parameterComparerMockSetup(parameterComparerMock);

        var genericFactory = ((IParameterMappingRepositoryFactory)nonGenericFactory).ForParameter(parameterComparerMock.Object);

        var repository = genericFactory.Create<TRecord, TData>();

        registrator(repository.Collector);

        return new(repository.Builder.Build());
    }

    public IParameterMapper<TParameter, TRecord, TData> Mapper { get; }

    public MapperContext(IParameterMapper<TParameter, TRecord, TData> mapper)
    {
        Mapper = mapper;
    }
}
