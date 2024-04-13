namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.ParameterMapperCases;

using Attribinter.Parameters.Representations;

using Moq;

using System.Collections.Generic;

internal interface IMapperFixture<TParameter, TParameterRepresentation, TRecord, TData>
{
    public abstract IParameterMapper<TParameter, TRecord, TData> Sut { get; }

    public abstract Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> ParameterRepresentationFactoryMock { get; }
    public abstract Mock<IEqualityComparer<TParameterRepresentation>> ParameterRepresentationComparerMock { get; }
}
