using System.Linq;
using System.Threading.Tasks;
using MediatR;
using PartyPlanner.Data.Entities;


namespace PartyPlanner.Api.Services.Cocktail
{
    public class GetCocktails
    {
        public class Query : IRequest<IQueryable<Data.Models.Cocktail>>
        {
            
        }

        public class Handler : AsyncRequestHandler<Query, IQueryable<Data.Models.Cocktail>>
        {
            private readonly PartyPlannerContext _context;

            public Handler(PartyPlannerContext context)
            {
                _context = context;
            }

            protected override async Task<IQueryable<Data.Models.Cocktail>> HandleCore(Query query)
            {
                return _context.Cocktails.Where(cocktail => cocktail.Active);
            }
        }
    }
}
