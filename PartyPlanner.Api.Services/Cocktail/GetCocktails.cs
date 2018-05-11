using System.Linq;
using System.Threading.Tasks;
using MediatR;
using PartyPlanner.Api.Services.Cocktail.Models;
using PartyPlanner.Data.Entities;


namespace PartyPlanner.Api.Services.Cocktail
{
    public class GetCocktails
    {
        public class Query : IRequest<IQueryable<CocktailDto>>
        {
            
        }

        public class Handler : AsyncRequestHandler<Query, IQueryable<CocktailDto>>
        {
            private readonly PartyPlannerContext _context;

            public Handler(PartyPlannerContext context)
            {
                _context = context;
            }

            protected override async Task<IQueryable<CocktailDto>> HandleCore(Query query)
            {
                return _context.Cocktails
                    .Where(cocktail => cocktail.Active)
                    .Select(cocktail => new CocktailDto
                    {
                        Id = cocktail.Id,
                        Amount = cocktail.Amount,
                        CreatedDate = cocktail.CreatedDate,
                        Degrees = cocktail.Degrees,
                        ImageSrc = cocktail.Image,
                        Name = cocktail.Name,
                        UpdatedDate = cocktail.UpdatedDate,
                        Username = cocktail.User.UserName
                    })
                    .OrderByDescending(cocktail => cocktail.UpdatedDate);
            }
        }
    }
}
