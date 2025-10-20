namespace Workout.Shared.Exceptions;

public class BusinessErrorException : Exception
{
    public BusinessErrorException(string message) : base(message)
    {
    }

    public BusinessErrorException(string message, Exception innerException) : base(message, innerException)
    {
    }
}