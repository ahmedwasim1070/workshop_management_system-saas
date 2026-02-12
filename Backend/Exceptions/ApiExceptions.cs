namespace Backend.Exceptions;

public class ApiException : Exception
{
    public Int16 StatusCode { get; }

    public ApiException(Int16 statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}