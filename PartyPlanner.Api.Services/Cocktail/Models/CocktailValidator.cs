using FluentValidation;

namespace PartyPlanner.Api.Services.Cocktail.Models
{
    public class CocktailValidator : AbstractValidator<Data.Models.Cocktail>
    {
        public CocktailValidator()
        {
            RuleFor(x => x.Id)
                .NotNull();
            RuleFor(x => x.Active)
                .NotNull();
            RuleFor(x => x.Amount)
                .NotNull()
                .WithMessage("field Amount is required");
            RuleFor(x => x.Amount)
                .ExclusiveBetween(10, 5000)
                .WithMessage("field Degrees must be at least 10 and not more than 5000");
            RuleFor(x => x.CreatedDate)
                .NotNull();
            RuleFor(x => x.Degrees)
                .NotNull()
                .WithMessage("field Amount is required"); 
            RuleFor(x => x.Degrees)
                .ExclusiveBetween(1, 100)
                .WithMessage("field Degrees must be at least 1 and not more than 100");
            RuleFor(x => x.Image)
                .NotNull()
                .WithMessage("field Amount is required");
            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("field Amount is required");
        }
    }
}
