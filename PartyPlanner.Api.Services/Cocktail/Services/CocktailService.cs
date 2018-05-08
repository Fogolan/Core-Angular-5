using System;
using PartyPlanner.Api.Services.Cocktail.Models;
using PartyPlanner.Data.Entities;
using PartyPlanner.Data.Models;

namespace PartyPlanner.Api.Services.Cocktail.Services
{
    public class CocktailService : ICocktailService
    {
        private readonly PartyPlannerContext _context;

        public CocktailService(PartyPlannerContext context)
        {
            _context = context;
        }

        public Data.Models.Cocktail MapCocktailDtoToCocktail(CocktailDto cocktailDto, UserIdentity user)
        {
            return new Data.Models.Cocktail
            {
                Name = cocktailDto.Name,
                Active = true,
                Amount = cocktailDto.Amount,
                CreatedDate = DateTime.Now,
                Degrees = cocktailDto.Degrees,
                Image = cocktailDto.Image,
                User = user
            };
        }

        public Data.Models.Cocktail MapCocktailDtoToCocktail(CocktailDto cocktailDto, UserIdentity user, Data.Models.Cocktail cocktail)
        {
            cocktail.Name = cocktailDto.Name;
            cocktail.Amount = cocktailDto.Amount;
            cocktail.Degrees = cocktailDto.Degrees;
            cocktail.Image = cocktailDto.Image;
            cocktail.CreatedDate = cocktailDto.CreatedDate;
            cocktail.Active = true;
            cocktail.User = user;
            cocktail.UpdatedDate = DateTime.Now;

            return cocktail;
        }
    }
}
