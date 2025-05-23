using CatalogService.Domain.Repositories;
using FluentValidation;
using FluentValidation.Results;

namespace CatalogService.Api.Dtos.Validators;

public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator(IProductRepository productRepository)
    {
        RuleFor(p => p.Name).NotEmpty()
            .When(p => p.Name is not null);
        RuleFor(p => p.Description).NotEmpty()
            .When(p => p.Description is not null);
        RuleFor(p => p.Category).NotEmpty()
            .When(p => p.Category is not null);
        RuleFor(p => p.Price!.Value).GreaterThan(0)
            .When(p => p.Price is not null);
        RuleFor(p => p.Quantity!.Value).GreaterThan(-1)
            .When(p => p.Quantity is not null);
    }
    
    protected override bool PreValidate(ValidationContext<UpdateProductRequest> context, ValidationResult result)
    {
        var properties = context.InstanceToValidate.GetType().GetProperties();

        if (properties.All(property => property.GetValue(context.InstanceToValidate) is null))
        {
            result.Errors.Add(new ValidationFailure("", "Не передано ни одно поле для обновления."));

            return false;
        }

        return true;
    }
}