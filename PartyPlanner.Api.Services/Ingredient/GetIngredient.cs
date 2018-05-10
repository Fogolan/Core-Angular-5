using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PartyPlanner.Api.Services.Ingredient.Models;
using PartyPlanner.Data.Entities;

namespace PartyPlanner.Api.Services.Ingredient
{
    public class GetIngredient
    {
        public class Query : IRequest<IngredientDto>
        {
            public int Id { get; set; }
        }

        public class Handler : AsyncRequestHandler<Query, IngredientDto>
        {
            private readonly PartyPlannerContext _context;

            public Handler(PartyPlannerContext context)
            {
                _context = context;
            }
            protected override async Task<IngredientDto> HandleCore(Query query)
            {
                return await _context.Ingredients
                    .Where(ingredient => ingredient.Active)
                    .Select(ingredient => new IngredientDto
                    {
                        Id = ingredient.Id,
                        Degrees = ingredient.Degrees,
                        ImageSrc = ingredient.ImageSrc,
                        Name = ingredient.Name,
                        UserName = ingredient.User.UserName,
                        UpdatedDate = ingredient.UpdatedDate
                    })
                    .FirstAsync(ingredient => ingredient.Id == query.Id);
            }
        }
    }
}