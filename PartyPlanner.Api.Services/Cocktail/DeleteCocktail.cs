using System.Linq;
using System.Threading.Tasks;
using MediatR;
using PartyPlanner.Data.Entities;

namespace PartyPlanner.Api.Services.Cocktail
{
    public class DeleteCocktail
    {
        public class Command : IRequest<int>
        {
            public int Id { get; set; }
        }

        public class Handler : AsyncRequestHandler<Command, int>
        {
            private readonly PartyPlannerContext _context;

            public Handler(PartyPlannerContext context)
            {
                _context = context;
            }

            protected override Task<int> HandleCore(Command command)
            {
                var cocktail = _context.Cocktails.First(c => c.Id == command.Id);
                cocktail.Active = false;
                _context.SaveChanges();
                return Task.FromResult(command.Id);
            }
        }
    }
}
