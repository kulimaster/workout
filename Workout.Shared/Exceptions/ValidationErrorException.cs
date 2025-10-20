namespace Workout.Shared.Exceptions;

public class ValidationErrorException : Exception
{
    public ValidationErrorException(string message) : base(message)
    {
    }

    public ValidationErrorException(string message, Exception innerException) : base(message, innerException)
    {
    }
}