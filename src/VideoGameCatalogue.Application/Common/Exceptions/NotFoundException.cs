namespace VideoGameCatalogue.Application.Common.Exceptions;

/// <summary>
/// Raised when a requested resource does not exist. Translated to a 404 by the API layer.
/// </summary>
public sealed class NotFoundException : Exception
{
    public NotFoundException(string resource, object key)
        : base($"{resource} with id '{key}' was not found.")
    {
    }
}
