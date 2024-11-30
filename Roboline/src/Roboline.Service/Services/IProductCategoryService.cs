using Roboline.Domain;
using Roboline.Domain.Contracts;

namespace Roboline.Service.Services;

public interface IProductCategoryService
{
    public Task<IEnumerable<ProductCategory>> GetAllCategoriesAsync(CancellationToken cancellationToken);
    public Task<ProductCategory> GetCategoryByIdAsync(int id, CancellationToken cancellationToken);
    public Task AddCategoryAsync(ProductCategoryRequest request, CancellationToken cancellationToken);
    public Task UpdateCategoryAsync(int id, ProductCategoryRequest request, CancellationToken cancellationToken);
    public Task DeleteCategoryAsync(int id, CancellationToken cancellationToken);
}