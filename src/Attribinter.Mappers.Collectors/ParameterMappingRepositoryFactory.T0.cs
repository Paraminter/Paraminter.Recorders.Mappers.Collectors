namespace Attribinter.Mappers.Collectors;

using System;

/// <inheritdoc cref="IParameterMappingRepositoryFactory"/>
public sealed class ParameterMappingRepositoryFactory : IParameterMappingRepositoryFactory
{
    /// <summary>Instantiates a <see cref="ParameterMappingRepositoryFactory"/>, handling creation of <see cref="IParameterMappingRepository{TParameter, TParameterRepresentation, TRecord, TData}"/>.</summary>
    public ParameterMappingRepositoryFactory() { }

    IParameterMappingRepositoryFactory<TParameter, TParameterRepresentation> IParameterMappingRepositoryFactory.WithParameterRepresentation<TParameter, TParameterRepresentation>(IParameterRepresentationFactory<TParameter, TParameterRepresentation> parameterRepresentationFactory, IParameterRepresentationEqualityComparer<TParameterRepresentation> parameterRepresentationComparer)
    {
        if (parameterRepresentationFactory is null)
        {
            throw new ArgumentNullException(nameof(parameterRepresentationFactory));
        }

        if (parameterRepresentationComparer is null)
        {
            throw new ArgumentNullException(nameof(parameterRepresentationComparer));
        }

        return new ParameterMappingRepositoryFactory<TParameter, TParameterRepresentation>(parameterRepresentationFactory, parameterRepresentationComparer);
    }
}
