using System.Linq;
using System.Threading.Tasks;
using MediatR;
using PartyPlanner.Data.Entities;

namespace PartyPlanner.Api.Services.Cocktail
{
    public class UpdateCocktail
    {
        public class Command : IRequest<int>
        {
            public Data.Models.Cocktail Cocktail { get; set; }
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
                var cocktailDto = command.Cocktail;
                var cocktail = _context.Cocktails
                    .First(c => c.Id == command.Cocktail.Id);
                cocktail.Name = cocktailDto.Name;
                cocktail.Amount = cocktailDto.Amount;
                cocktail.Degrees = cocktailDto.Degrees;
                cocktail.Image = cocktailDto.Image;
                cocktail.UpdatedDate = cocktailDto.UpdatedDate;
                _context.SaveChanges();
                return Task.FromResult(cocktail.Id);
            }
        }
    }
}
