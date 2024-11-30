using Roboline.Domain;
using Roboline.Domain.Contracts;

namespace Roboline.Service.Services;

public interface IProductService
{
    public Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken cancellationToken);
    public Task<Product> GetProductByIdAsync(int id, CancellationToken cancellationToken);
    public Task AddProductAsync(ProductRequest request, CancellationToken cancellationToken);
    public Task UpdateProductAsync(int id, ProductRequest request, CancellationToken cancellationToken);
    public Task DeleteProductAsync(int id, CancellationToken cancellationToken);
}