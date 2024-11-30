namespace Roboline.Domain.Interfaces;

public interface IProductRepository
{
    public Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken);
    public Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken);
    public Task AddAsync(Product product, CancellationToken cancellationToken);
    public Task UpdateAsync(Product product, CancellationToken cancellationToken);
    public Task DeleteAsync(Product product, CancellationToken cancellationToken);
}