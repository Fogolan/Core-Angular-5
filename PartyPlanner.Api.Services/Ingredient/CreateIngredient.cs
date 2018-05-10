using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using PartyPlanner.Api.Services.Cocktail;
using PartyPlanner.Api.Services.Ingredient.Models;
using PartyPlanner.Api.Services.User.Services;
using PartyPlanner.Data.Entities;

namespace PartyPlanner.Api.Services.Ingredient
{
    public class CreateIngredient
    {
        public class Command : IRequest<int>
        {
            public IngredientDto Ingredient { get; set; }

            public ClaimsPrincipal UserClaims { get; set; }
        }

        public class Handler : AsyncRequestHandler<Command, int>, IPipelineBehavior<Command, int>
        {
            private readonly PartyPlannerContext _context;
            private readonly IValidator<IngredientDto> _validator;
            private readonly IUserService userService;

            public Handler(PartyPlannerContext context, IValidator<IngredientDto> validator, IUserService userService)
            {
                _context = context;
                _validator = validator;
                this.userService = userService;
            }

            protected override async Task<int> HandleCore(Command command)
            {
                var currentUser = await userService.GetUserIdentity(command.UserClaims);

                var ingredient = new Data.Models.Ingredient
                {
                    Degrees = command.Ingredient.Degrees,
                    ImageSrc = command.Ingredient.ImageSrc,
                    Name = command.Ingredient.Name,
                    User = currentUser,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    Active = true
                };

                _context.Ingredients.Add(ingredient);
                await _context.SaveChangesAsync();

                return ingredient.Id;
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
