using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using PartyPlanner.Api.Services.Cocktail.Models;
using PartyPlanner.Api.Services.Cocktail.Services;
using PartyPlanner.Api.Services.User.Services;
using PartyPlanner.Data.Entities;
using PartyPlanner.Data.Models;

namespace PartyPlanner.Api.Services.Cocktail
{
    public class CreateCocktail
    {
        public class Command : IRequest<int>
        {
            public CocktailDto CocktailDto { get; set; }

            public ClaimsPrincipal UserClaims { get; set; }
        }

        public class Handler : AsyncRequestHandler<Command, int>
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
                try
                {
                    var user = await _userService.GetUserIdentity(command.UserClaims);
                    var cocktail = _cocktailtService.MapCocktailDtoToCocktail(command.CocktailDto, user);
                    var recipes = command.CocktailDto.Ingredients.Select(ingredient => new Recipe
                    {
                        Cocktail = cocktail,
                        IngredientId = ingredient.Id
                    });

                    await _context.Cocktails.AddAsync(cocktail);
                    await _context.Recipes.AddRangeAsync(recipes);

                    await _context.SaveChangesAsync();

                    return cocktail.Id;
                }
                catch (Exception e)
                {

                    throw;
                }               
            }
        }
    }
}
