using FluentValidation;

namespace PartyPlanner.Api.Services.Ingredient.Models
{
    public class IngredientValidator : AbstractValidator<IngredientDto>
    {
        public IngredientValidator()
        {
            RuleFor(x => x.Id)
                .NotNull();
            RuleFor(x => x.Degrees)
                .GreaterThan(0)
                .LessThan(100);
            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("field Amount is required");
        }
    }
}
