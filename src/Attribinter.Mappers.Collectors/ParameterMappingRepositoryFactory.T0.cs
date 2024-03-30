namespace Attribinter.Mappers.Collectors;

using Attribinter.Parameters.Representations;

using System;

/// <inheritdoc cref="IParameterMappingRepositoryFactory"/>
public sealed class ParameterMappingRepositoryFactory : IParameterMappingRepositoryFactory
{
    /// <summary>Instantiates a <see cref="ParameterMappingRepositoryFactory"/>, handling creation of <see cref="IParameterMappingRepository{TParameter, TParameterRepresentation, TRecord, TData}"/>.</summary>
    public ParameterMappingRepositoryFactory() { }

    IParameterMappingRepositoryFactory<TParameter, TParameterRepresentation> IParameterMappingRepositoryFactory.WithParameterRepresentation<TParameter, TParameterRepresentation>(IParameterRepresentationFactory<TParameter, TParameterRepresentation> parameterRepresentationFactory)
    {
        if (parameterRepresentationFactory is null)
        {
            throw new ArgumentNullException(nameof(parameterRepresentationFactory));
        }

        return new ParameterMappingRepositoryFactory<TParameter, TParameterRepresentation>(parameterRepresentationFactory);
    }
}
