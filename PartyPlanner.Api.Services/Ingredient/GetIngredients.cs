using System.Linq;
using System.Threading.Tasks;
using MediatR;
using PartyPlanner.Api.Services.Ingredient.Models;
using PartyPlanner.Data.Entities;

namespace PartyPlanner.Api.Services.Ingredient
{
    public class GetIngredients
    {
        public class Query : IRequest<IQueryable<IngredientDto>>
        {
        }

        public class Handler : AsyncRequestHandler<Query, IQueryable<IngredientDto>>
        {
            private readonly PartyPlannerContext context;

            public Handler(PartyPlannerContext context)
            {
                this.context = context;
            }
            protected override async Task<IQueryable<IngredientDto>> HandleCore(Query query)
            {
                return await Task.FromResult(context.Ingredients
                    .Where(ingredient => ingredient.Active)
                    .Select(ingredient => new IngredientDto
                    {
                        Id = ingredient.Id,
                        Degrees = ingredient.Degrees,
                        ImageSrc = ingredient.ImageSrc,
                        Name = ingredient.Name,
                        UserName = ingredient.User.UserName,
                        UpdatedDate = ingredient.UpdatedDate
                    }));
            }
        }
    }
}