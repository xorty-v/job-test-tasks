using Microsoft.EntityFrameworkCore;
using Roboline.Domain;
using Roboline.Domain.Interfaces;

namespace Roboline.Persistence.Repositories;

public class ProductCategoryRepository : IProductCategoryRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProductCategoryRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ProductCategory>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.ProductCategories
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<ProductCategory> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.ProductCategories.SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task AddAsync(ProductCategory category, CancellationToken cancellationToken)
    {
        _dbContext.ProductCategories.Add(category);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ProductCategory category, CancellationToken cancellationToken)
    {
        _dbContext.ProductCategories.Update(category);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ProductCategory category, CancellationToken cancellationToken)
    {
        _dbContext.ProductCategories.Remove(category);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}