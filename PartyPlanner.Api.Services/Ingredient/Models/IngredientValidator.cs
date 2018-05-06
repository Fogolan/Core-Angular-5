using FluentValidation;

namespace PartyPlanner.Api.Services.Ingredient.Models
{
    public class IngredientValidator : AbstractValidator<Data.Models.Ingredient>
    {
        public IngredientValidator()
        {
            RuleFor(x => x.Id);
            RuleFor(x => x.Active)
                .NotNull();
            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("field Amount is required");
        }
    }
}
