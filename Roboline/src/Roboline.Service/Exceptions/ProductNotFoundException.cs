namespace Roboline.Service.Exceptions.ProductErrors;

public sealed class ProductNotFoundException : NotFoundException
{
    internal ProductNotFoundException() : base("Product not found")
    {
    }
}