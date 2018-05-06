using System.Linq;
using System.Threading.Tasks;
using MediatR;
using PartyPlanner.Data.Entities;

namespace PartyPlanner.Api.Services.Ingredient
{
    public class DeleteIngredient
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
                var ingredient = _context.Ingredients.First(i => i.Id == command.Id);
                _context.Remove(ingredient);
                _context.SaveChanges();
                return Task.FromResult(command.Id);
            }
        }
    }
}
