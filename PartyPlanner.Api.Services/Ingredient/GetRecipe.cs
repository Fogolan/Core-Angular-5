using System.Linq;
using System.Threading.Tasks;
using MediatR;
using PartyPlanner.Data.Entities;

namespace PartyPlanner.Api.Services.Ingredient
{
    public class GetRecipe
    {
        public class Query : IRequest<IQueryable<Data.Models.Ingredient>>
        {
            public int CocktailId { get; set; }
        }

        public class Handler : AsyncRequestHandler<Query, IQueryable<Data.Models.Ingredient>>
        {
            private readonly PartyPlannerContext _context;

            public Handler(PartyPlannerContext context)
            {
                _context = context;
            }

            protected override Task<IQueryable<Data.Models.Ingredient>> HandleCore(Query query)
            {
                return Task.FromResult(_context.Recipes
                    .Where(recipe => recipe.CocktailId == query.CocktailId)
                    .Select(recipe => recipe.Ingredient));
            }
        }
    }
}

