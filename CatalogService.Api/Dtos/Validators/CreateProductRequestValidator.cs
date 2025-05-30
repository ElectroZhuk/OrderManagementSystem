using CatalogService.Domain.Repositories;
using FluentValidation;

namespace CatalogService.Api.Dtos.Validators;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator(IProductRepository productRepository)
    {
        RuleFor(p => p.Name).NotEmpty();
        RuleFor(p => p.Description).NotEmpty();
        RuleFor(p => p.Category).NotEmpty();
        RuleFor(p => p.Price).GreaterThan(0);
        RuleFor(p => p.Quantity).GreaterThanOrEqualTo(0);
    }
}