using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using PartyPlanner.Api.Services.Cocktail.Models;
using PartyPlanner.Api.Services.User.Services;
using PartyPlanner.Data.Entities;

namespace PartyPlanner.Api.Services.Cocktail
{
    public class GetUserCocktails
    {
        public class Query : IRequest<IQueryable<CocktailDto>>
        {
            public ClaimsPrincipal UserClaims { get; set; }
        }

        public class Handler : AsyncRequestHandler<Query, IQueryable<CocktailDto>>
        {
            private readonly PartyPlannerContext context;
            private readonly IUserService userService;

            public Handler(PartyPlannerContext context, IUserService userService)
            {
                this.context = context;
                this.userService = userService;
            }
            protected override async Task<IQueryable<CocktailDto>> HandleCore(Query query)
            {
                var user = await userService.GetUserIdentity(query.UserClaims);

                return context.Cocktails
                    .Where(cocktail => cocktail.User.Id == user.Id)
                    .OrderByDescending(cocktail => cocktail.UpdatedDate)
                    .Select(cocktail => new CocktailDto
                    {
                        Id = cocktail.Id,
                        Degrees = cocktail.Degrees,
                        ImageSrc = cocktail.Image,
                        Name = cocktail.Name,
                        Username = cocktail.User.UserName,
                        UpdatedDate = cocktail.UpdatedDate
                    });
            }
        }
    }
}