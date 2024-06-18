namespace Paraminter.Recorders.Mappers.Collectors.ArgumentExistenceRecorderMapperBuilder;

using Moq;

using Paraminter.Parameters.Representations;

using System.Collections.Generic;

internal interface IFixture<TParameter, TParameterRepresentation, TRecord>
{
    public abstract IArgumentExistenceRecorderMapperBuilder<TParameter, TRecord> Sut { get; }

    public abstract Mock<IParameterRepresentationFactory<TParameter, TParameterRepresentation>> ParameterRepresentationFactoryMock { get; }
    public abstract Mock<IEqualityComparer<TParameterRepresentation>> ParameterRepresentationComparerMock { get; }
}
