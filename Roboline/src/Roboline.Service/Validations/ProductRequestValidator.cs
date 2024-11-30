using FluentValidation;
using Roboline.Domain.Contracts;

namespace Roboline.Service.Validations;

public class ProductRequestValidator : NameDescriptionRequestValidator<ProductRequest>
{
    public ProductRequestValidator()
    {
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");
    }
}