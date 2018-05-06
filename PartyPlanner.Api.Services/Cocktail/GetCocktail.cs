using System.Linq;
using System.Threading.Tasks;
using MediatR;
using PartyPlanner.Data.Entities;

namespace PartyPlanner.Api.Services.Cocktail
{
    public class GetCocktail
    {
        public class Query : IRequest<Data.Models.Cocktail>
        {
            public int Id { get; set; }
        }

        public class Handler : AsyncRequestHandler<Query, Data.Models.Cocktail>
        {
            private readonly PartyPlannerContext _context;

            public Handler(PartyPlannerContext context)
            {
                _context = context;
            }
            protected override async Task<Data.Models.Cocktail> HandleCore(Query query)
            {
                var result = _context.Cocktails
                    .Where(cocktail => cocktail.Active)
                    .First(cocktail => cocktail.Id == query.Id);
                return result;
            }
        }
    }
}
