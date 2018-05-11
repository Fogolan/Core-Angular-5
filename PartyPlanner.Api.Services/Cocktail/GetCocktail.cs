using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PartyPlanner.Api.Services.Cocktail.Models;
using PartyPlanner.Api.Services.Ingredient.Models;
using PartyPlanner.Data.Entities;

namespace PartyPlanner.Api.Services.Cocktail
{
    public class GetCocktail
    {
        public class Query : IRequest<CocktailDto>
        {
            public int Id { get; set; }
        }

        public class Handler : AsyncRequestHandler<Query, CocktailDto>
        {
            private readonly PartyPlannerContext _context;

            public Handler(PartyPlannerContext context)
            {
                _context = context;
            }
            protected override async Task<CocktailDto> HandleCore(Query query)
            {
                var result = await _context.Cocktails
                    .Where(cocktail => cocktail.Active)
                    .Select(cocktail => new CocktailDto
                    {
                        Id = cocktail.Id,
                        Amount = cocktail.Amount,
                        CreatedDate = cocktail.CreatedDate,
                        UpdatedDate = cocktail.UpdatedDate,
                        Name = cocktail.Name,
                        Degrees = cocktail.Degrees,
                        ImageSrc = cocktail.Image,
                        Ingredients = cocktail.Recipes.Select(recipe => new IngredientDto
                        {
                            Id = recipe.Ingredient.Id,
                            Degrees = recipe.Ingredient.Degrees,
                            ImageSrc = recipe.Ingredient.ImageSrc,
                            Name = recipe.Ingredient.Name,
                            UserName = recipe.Ingredient.User.UserName
                        }).ToList()
                    })
                    .FirstAsync(cocktail => cocktail.Id == query.Id);
                return result;
            }
        }
    }
}
