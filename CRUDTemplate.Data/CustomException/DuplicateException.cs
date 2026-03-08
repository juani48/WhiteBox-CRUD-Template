namespace CRUDTemplate.Data.CustomException;

/// <summary>
/// Represents an exception that is thrown when a duplicate entity is detected.
/// </summary>
public class DuplicateException : Exception
{
    public DuplicateException(string message) : base(message) {}
}