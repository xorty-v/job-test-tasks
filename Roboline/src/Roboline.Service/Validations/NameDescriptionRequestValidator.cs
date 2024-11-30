using FluentValidation;
using Roboline.Domain.Contracts;

namespace Roboline.Service.Validations;

public class NameDescriptionRequestValidator<T> : AbstractValidator<T> where T : NameDescriptionRequest
{
    public NameDescriptionRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Description).MaximumLength(500).WithMessage("Description must be less than 500 characters.");
    }
}