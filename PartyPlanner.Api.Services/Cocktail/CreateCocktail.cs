using System;
using System.Threading.Tasks;
using MediatR;
using PartyPlanner.Data.Entities;

namespace PartyPlanner.Api.Services.Cocktail
{
    public class CreateCocktail
    {
        public class Command : IRequest<int>
        {
            public Data.Models.Cocktail Cocktail { get; set; }
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
                command.Cocktail.Active = true;
                command.Cocktail.CreatedDate = DateTime.Now;
                _context.Add(command.Cocktail);
                _context.SaveChanges();
                return Task.FromResult(command.Cocktail.Id);
            }
        }
    }
}
