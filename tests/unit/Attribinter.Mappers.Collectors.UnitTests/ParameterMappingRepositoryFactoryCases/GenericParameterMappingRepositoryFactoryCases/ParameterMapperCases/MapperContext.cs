namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.GenericParameterMappingRepositoryFactoryCases.ParameterMapperCases;

using Moq;

using System;
using System.Collections.Generic;

internal sealed class MapperContext<TParameter, TParameterRepresentation, TRecord, TData>
{
    public static MapperContext<TParameter, TParameterRepresentation, TRecord, TData> Create(Action<Mock<IEqualityComparer<TParameterRepresentation>>> parameterComparerMockSetup, Action<IParameterMappingCollector<TParameterRepresentation, TRecord, TData>> registrator)
    {
        ParameterMappingRepositoryFactory nonGenericFactory = new();

        Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> parameterRepresentationFactoryMock = new() { DefaultValue = DefaultValue.Mock };
        Mock<IEqualityComparer<TParameterRepresentation>> parameterComparerMock = new();

        parameterComparerMockSetup(parameterComparerMock);

        var genericFactory = ((IParameterMappingRepositoryFactory)nonGenericFactory).ForParameter(parameterRepresentationFactoryMock.Object, parameterComparerMock.Object);

        var repository = genericFactory.Create<TRecord, TData>();

        registrator(repository.Collector);

        var mapper = repository.Builder.Build();

        return new(mapper, parameterRepresentationFactoryMock, parameterComparerMock);
    }

    public IParameterMapper<TParameter, TRecord, TData> Mapper { get; }

    public Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> ParameterRepresentationFactoryMock { get; }
    public Mock<IEqualityComparer<TParameterRepresentation>> ParameterComparerMock { get; }

    public MapperContext(IParameterMapper<TParameter, TRecord, TData> mapper, Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> parameterRepresentationFactoryMock, Mock<IEqualityComparer<TParameterRepresentation>> parameterComparerMock)
    {
        Mapper = mapper;

        ParameterRepresentationFactoryMock = parameterRepresentationFactoryMock;
        ParameterComparerMock = parameterComparerMock;
    }
}
