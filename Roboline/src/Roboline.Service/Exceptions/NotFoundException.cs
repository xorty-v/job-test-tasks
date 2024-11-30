namespace Roboline.Service.Exceptions;

public abstract class NotFoundException : Exception
{
    protected internal NotFoundException(string message) : base(message)
    {
    }
}