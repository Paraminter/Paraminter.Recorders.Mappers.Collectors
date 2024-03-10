namespace Attribinter.Mappers.Collectors.ParameterMapperFactoryCases;

using Moq;

internal sealed class FactoryContext
{
    public static FactoryContext Create()
    {
        Mock<IParameterMappingRepositoryFactory> repositoryFactoryMock = new() { DefaultValue = DefaultValue.Mock };

        ParameterMapperFactory factory = new(repositoryFactoryMock.Object);

        return new(factory, repositoryFactoryMock);
    }

    public ParameterMapperFactory Factory { get; }

    public Mock<IParameterMappingRepositoryFactory> RepositoryFactoryMock { get; }

    private FactoryContext(ParameterMapperFactory factory, Mock<IParameterMappingRepositoryFactory> repositoryFactoryMock)
    {
        Factory = factory;

        RepositoryFactoryMock = repositoryFactoryMock;
    }
}
