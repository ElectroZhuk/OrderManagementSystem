using CatalogService.Domain.Repositories;
using FluentValidation;
using FluentValidation.Results;

namespace CatalogService.Api.Dtos.Validators;

public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator(IProductRepository productRepository)
    {
        RuleFor(p => p.Id).NotEmpty();
        RuleFor(p => p.Name).NotEmpty();
        RuleFor(p => p.Description).NotEmpty();
        RuleFor(p => p.Category).NotEmpty();
        RuleFor(p => p.Price).GreaterThan(0);
        RuleFor(p => p.Quantity).GreaterThanOrEqualTo(0);
    }
}