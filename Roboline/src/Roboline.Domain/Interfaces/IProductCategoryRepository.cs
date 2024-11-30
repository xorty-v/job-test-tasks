namespace Roboline.Domain.Interfaces;

public interface IProductCategoryRepository
{
    public Task<IEnumerable<ProductCategory>> GetAllAsync(CancellationToken cancellationToken);
    public Task<ProductCategory> GetByIdAsync(int id, CancellationToken cancellationToken);
    public Task AddAsync(ProductCategory category, CancellationToken cancellationToken);
    public Task UpdateAsync(ProductCategory category, CancellationToken cancellationToken);
    public Task DeleteAsync(ProductCategory category, CancellationToken cancellationToken);
}