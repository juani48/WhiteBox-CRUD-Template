namespace CRUDTemplate.Data.CustomException;

/// <summary>
/// Represents an exception that is thrown when an entity is not found.
/// </summary>
public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}