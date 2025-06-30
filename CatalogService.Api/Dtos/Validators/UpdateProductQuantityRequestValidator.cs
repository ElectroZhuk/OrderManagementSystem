using FluentValidation;

namespace CatalogService.Api.Dtos.Validators;

public class UpdateProductQuantityRequestValidator : AbstractValidator<UpdateProductQuantityRequest>
{
    public UpdateProductQuantityRequestValidator()
    {
        RuleFor(r => r.DecreaseAmount).GreaterThan(0);
    }
}