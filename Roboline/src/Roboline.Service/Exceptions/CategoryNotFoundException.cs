namespace Roboline.Service.Exceptions;

public class CategoryNotFoundException : NotFoundException
{
    internal CategoryNotFoundException() : base("Category not found")
    {
    }
}