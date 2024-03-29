namespace Attribinter.Mappers.Collectors;

using System.Collections.Generic;

/// <summary>Determines equality when comparing parameter representations.</summary>
/// <typeparam name="TParameterRepresentation">The type used as parameter representation.</typeparam>
public interface IParameterRepresentationEqualityComparer<in TParameterRepresentation> : IEqualityComparer<TParameterRepresentation> { }
