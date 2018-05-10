using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using PartyPlanner.Api.Services.Cocktail.Models;
using PartyPlanner.Api.Services.Cocktail.Services;
using PartyPlanner.Api.Services.User.Services;
using PartyPlanner.Data.Entities;

namespace PartyPlanner.Api.Services.Cocktail
{
    public class CreateCocktail
    {
        public class Command : IRequest<int>
        {
            public CocktailDto CocktailDto { get; set; }

            public ClaimsPrincipal UserClaims { get; set; }
        }

        public class Handler : AsyncRequestHandler<Command, int>, IPipelineBehavior<Command, int>
        {
            private readonly ICocktailService _cocktailtService;
            private readonly PartyPlannerContext _context;
            private readonly IValidator<CocktailDto> _validator;
            private readonly IUserService _userService;

            public Handler(PartyPlannerContext context, IValidator<CocktailDto> validator,
                ICocktailService cocktailtService, IUserService userService)
            {
                _context = context;
                _validator = validator;
                _cocktailtService = cocktailtService;
                _userService = userService;
            }

            protected override async Task<int> HandleCore(Command command)
            {
                var user = await _userService.GetUserIdentity(command.UserClaims);
                var cocktail = _cocktailtService.MapCocktailDtoToCocktail(command.CocktailDto, user);

                _context.Cocktails.Add(cocktail);

                await _context.SaveChangesAsync();

                return cocktail.Id;
            }

            public async Task<int> Handle(Command request, CancellationToken cancellationToken, RequestHandlerDelegate<int> next)
            {
                _validator.ValidateAndThrow(request.CocktailDto);

                var response = await next();

                return response;
            }
        }
    }
}
