using Roboline.Domain;
using Roboline.Domain.Contracts;
using Roboline.Domain.Interfaces;
using Roboline.Service.Exceptions;

namespace Roboline.Service.Services;

public class ProductCategoryService : IProductCategoryService
{
    private readonly IProductCategoryRepository _categoryRepository;

    public ProductCategoryService(IProductCategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<ProductCategory>> GetAllCategoriesAsync(CancellationToken cancellationToken)
    {
        return await _categoryRepository.GetAllAsync(cancellationToken);
    }

    public async Task<ProductCategory> GetCategoryByIdAsync(int id, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(id, cancellationToken);

        if (category == null)
        {
            throw new CategoryNotFoundException();
        }

        return category;
    }

    public async Task AddCategoryAsync(ProductCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = new ProductCategory
        {
            Name = request.Name,
            Description = request.Description
        };

        await _categoryRepository.AddAsync(category, cancellationToken);
    }

    public async Task UpdateCategoryAsync(int id, ProductCategoryRequest request,
        CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(id, cancellationToken);

        if (category == null)
        {
            throw new CategoryNotFoundException();
        }

        category.Name = request.Name;
        category.Description = request.Description;

        await _categoryRepository.UpdateAsync(category, cancellationToken);
    }

    public async Task DeleteCategoryAsync(int id, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(id, cancellationToken);

        if (category == null)
        {
            throw new CategoryNotFoundException();
        }

        await _categoryRepository.DeleteAsync(category, cancellationToken);
    }
}