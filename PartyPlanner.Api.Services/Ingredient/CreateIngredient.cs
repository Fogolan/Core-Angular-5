using System.Threading.Tasks;
using MediatR;
using PartyPlanner.Data.Entities;

namespace PartyPlanner.Api.Services.Ingredient
{
    public class CreateIngredient
    {
        public class Command : IRequest<int>
        {
            public Data.Models.Ingredient Ingredient { get; set; }
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
                command.Ingredient.Active = true;
                _context.Ingredients.Add(command.Ingredient);
                _context.SaveChanges();

                return Task.FromResult(command.Ingredient.Id);
            }
        }
    }
}
