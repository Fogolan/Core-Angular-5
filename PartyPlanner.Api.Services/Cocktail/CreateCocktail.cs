using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
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

        public class Handler : AsyncRequestHandler<Command, int>, IPipelineBehavior<Command, int>
        {
            private readonly PartyPlannerContext _context;
            private readonly IValidator<Data.Models.Cocktail> _validator;

            public Handler(PartyPlannerContext context, IValidator<Data.Models.Cocktail> validator)
            {
                _context = context;
                _validator = validator;
            }

            protected override Task<int> HandleCore(Command command)
            {
                command.Cocktail.Active = true;
                command.Cocktail.CreatedDate = DateTime.Now;
                _context.Add(command.Cocktail);
                _context.SaveChanges();
                return Task.FromResult(command.Cocktail.Id);
            }

            public async Task<int> Handle(Command request, CancellationToken cancellationToken, RequestHandlerDelegate<int> next)
            {
                _validator.ValidateAndThrow(request.Cocktail);

                var response = await next();

                return response;
            }
        }
    }
}
