namespace Paraminter.Recorders.Mappers.Collectors.ArgumentExistenceRecorderMappingRepositoryFactoryCases.ArgumentExistenceRecorderMapperBuilderCases;

using Moq;

using Paraminter.Parameters.Representations;

using System.Collections.Generic;

internal interface IBuilderFixture<TParameter, TParameterRepresentation, TRecord>
{
    public abstract IArgumentExistenceRecorderMapperBuilder<TParameter, TRecord> Sut { get; }

    public abstract Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> ParameterRepresentationFactoryMock { get; }
    public abstract Mock<IEqualityComparer<TParameterRepresentation>> ParameterRepresentationComparerMock { get; }
}
