using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using PartyPlanner.Api.Services.Cocktail;
using PartyPlanner.Data.Entities;

namespace PartyPlanner.Api.Services.Ingredient
{
    public class CreateIngredient
    {
        public class Command : IRequest<int>
        {
            public Data.Models.Ingredient Ingredient { get; set; }
        }

        public class Handler : AsyncRequestHandler<Command, int>, IPipelineBehavior<Command, int>
        {
            private readonly PartyPlannerContext _context;
            private readonly IValidator<Data.Models.Ingredient> _validator;

            public Handler(PartyPlannerContext context, IValidator<Data.Models.Ingredient> validator)
            {
                _context = context;
                _validator = validator;
            }

            protected override Task<int> HandleCore(Command command)
            {
                command.Ingredient.Active = true;
                _context.Ingredients.Add(command.Ingredient);
                _context.SaveChanges();

                return Task.FromResult(command.Ingredient.Id);
            }

            public async Task<int> Handle(CreateIngredient.Command request, CancellationToken cancellationToken, RequestHandlerDelegate<int> next)
            {
                _validator.ValidateAndThrow(request.Ingredient);

                var response = await next();

                return response;
            }
        }
    }
}
