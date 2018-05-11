using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PartyPlanner.Api.Services.Cocktail.Models;
using PartyPlanner.Data.Entities;
using PartyPlanner.Data.Models;

namespace PartyPlanner.Api.Services.Cocktail
{
    public class UpdateCocktail
    {
        public class Command : IRequest<int>
        {
            public CocktailDto Cocktail { get; set; }
            public int Id { get; set; }
        }

        public class Handler : AsyncRequestHandler<Command, int>
        {
            private readonly PartyPlannerContext _context;

            public Handler(PartyPlannerContext context)
            {
                _context = context;
            }

            protected override async Task<int> HandleCore(Command command)
            {
                var cocktailDto = command.Cocktail;
                var cocktail = _context.Cocktails
                    .First(c => c.Id == command.Cocktail.Id);
                var recipesIds = cocktailDto.Ingredients.Select(i => i.Id).ToList();
                var oldRecipes = await _context.Recipes
                    .Where(recipe => recipe.CocktailId == cocktail.Id)
                    .Where(recipe => !recipesIds.Contains(recipe.IngredientId))
                    .ToListAsync();
                _context.Recipes.RemoveRange(oldRecipes);

                cocktail.Name = cocktailDto.Name;
                cocktail.Amount = cocktailDto.Amount;
                cocktail.Degrees = cocktailDto.Degrees;
                cocktail.Image = cocktailDto.ImageSrc;
                cocktail.UpdatedDate = cocktailDto.UpdatedDate;

                var recipes = cocktailDto.Ingredients.Select(ingredient => new Recipe
                {
                    Cocktail = cocktail,
                    IngredientId = ingredient.Id
                }).ToList();

                await _context.Recipes.AddRangeAsync(recipes);
                await _context.SaveChangesAsync();
                return cocktail.Id;
            }
        }
    }
}
