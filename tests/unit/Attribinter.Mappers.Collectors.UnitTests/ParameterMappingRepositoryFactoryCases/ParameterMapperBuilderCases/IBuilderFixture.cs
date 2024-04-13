namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.ParameterMapperBuilderCases;

using Attribinter.Parameters.Representations;

using Moq;

using System.Collections.Generic;

internal interface IBuilderFixture<TParameter, TParameterRepresentation, TRecord, TData>
{
    public abstract IParameterMapperBuilder<TParameter, TRecord, TData> Sut { get; }

    public abstract Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> ParameterRepresentationFactoryMock { get; }
    public abstract Mock<IEqualityComparer<TParameterRepresentation>> ParameterRepresentationComparerMock { get; }
}
