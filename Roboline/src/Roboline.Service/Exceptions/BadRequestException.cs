namespace Roboline.Service.Exceptions;

public abstract class BadRequestException : Exception
{
    protected internal BadRequestException(string message) : base(message)
    {
    }
}