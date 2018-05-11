using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using PartyPlanner.Api.Services.Ingredient.Models;
using PartyPlanner.Api.Services.User.Services;
using PartyPlanner.Data.Entities;

namespace PartyPlanner.Api.Services.User
{
    public class GetUserIngredients
    {
        public class Query : IRequest<IQueryable<IngredientDto>>
        {
            public ClaimsPrincipal UserClaims { get; set; }
        }

        public class Handler : AsyncRequestHandler<Query, IQueryable<IngredientDto>>
        {
            private readonly PartyPlannerContext context;
            private readonly IUserService userService;

            public Handler(PartyPlannerContext context, IUserService userService)
            {
                this.context = context;
                this.userService = userService;
            }
            protected override async Task<IQueryable<IngredientDto>> HandleCore(Query query)
            {
                var user = await userService.GetUserIdentity(query.UserClaims);

                return context.Ingredients
                    .Where(ingredient => ingredient.Active)
                    .Where(ingredient => ingredient.User.Id == user.Id)
                    .OrderByDescending(ingredient => ingredient.UpdatedDate)
                    .Select(ingredient => new IngredientDto
                    {
                        Id = ingredient.Id,
                        Degrees = ingredient.Degrees,
                        ImageSrc = ingredient.ImageSrc,
                        Name = ingredient.Name,
                        UserName = ingredient.User.UserName,
                        UpdatedDate = ingredient.UpdatedDate
                    });
            }
        }
    }
}