namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.T2.ParameterMapperCases;

using Attribinter.Parameters.Representations;

using Moq;

using System;

internal sealed class MapperContext<TParameter, TParameterRepresentation, TRecord, TData>
{
    public static MapperContext<TParameter, TParameterRepresentation, TRecord, TData> Create(Action<Mock<IParameterRepresentationEqualityComparer<TParameterRepresentation>>> parameterRepresentationComparerMockSetup, Action<IParameterMappingCollector<TParameterRepresentation, TRecord, TData>> registrator)
    {
        IParameterMappingRepositoryFactory factory = new ParameterMappingRepositoryFactory();

        Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> parameterRepresentationFactoryMock = new() { DefaultValue = DefaultValue.Mock };
        Mock<IParameterRepresentationEqualityComparer<TParameterRepresentation>> parameterRepresentationComparerMock = new();

        parameterRepresentationComparerMockSetup(parameterRepresentationComparerMock);

        var repository = factory.WithParameterRepresentation(parameterRepresentationFactoryMock.Object).Create<TRecord, TData>(parameterRepresentationComparerMock.Object);

        registrator(repository.Collector);

        var mapper = repository.Builder.Build();

        return new(mapper, parameterRepresentationFactoryMock, parameterRepresentationComparerMock);
    }

    public IParameterMapper<TParameter, TRecord, TData> Mapper { get; }

    public Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> ParameterRepresentationFactoryMock { get; }
    public Mock<IParameterRepresentationEqualityComparer<TParameterRepresentation>> ParameterRepresentationComparerMock { get; }

    public MapperContext(IParameterMapper<TParameter, TRecord, TData> mapper, Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> parameterRepresentationFactoryMock, Mock<IParameterRepresentationEqualityComparer<TParameterRepresentation>> parameterRepresentationComparerMock)
    {
        Mapper = mapper;

        ParameterRepresentationFactoryMock = parameterRepresentationFactoryMock;
        ParameterRepresentationComparerMock = parameterRepresentationComparerMock;
    }
}
