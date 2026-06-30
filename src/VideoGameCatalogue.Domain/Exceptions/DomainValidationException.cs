namespace VideoGameCatalogue.Domain.Exceptions;

/// <summary>
/// Raised when an operation would put a domain entity into an invalid state.
/// The API layer translates this into a 400 response, keeping HTTP concerns out of the domain.
/// </summary>
public sealed class DomainValidationException : Exception
{
    public DomainValidationException(string message) : base(message)
    {
    }
}
