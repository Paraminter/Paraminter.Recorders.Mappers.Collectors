namespace Attribinter.Mappers.Collectors;

/// <summary>Handles creation of parameter representations.</summary>
/// <typeparam name="TParameter">The type of the parameters.</typeparam>
/// <typeparam name="TParameterRepresentation">The type used as a representation of the parameters.</typeparam>
public interface IParameterRepresentationFactory<in TParameter, out TParameterRepresentation>
{
    /// <summary>Creates a representation of the provided parameter.</summary>
    /// <param name="parameter">The parameter that is represented by the created parameter representation.</param>
    /// <returns>The created parameter representation.</returns>
    public abstract TParameterRepresentation Create(TParameter parameter);
}
