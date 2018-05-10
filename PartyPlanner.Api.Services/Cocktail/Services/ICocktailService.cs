using System;
using System.Collections.Generic;
using System.Text;
using PartyPlanner.Api.Services.Cocktail.Models;
using PartyPlanner.Data.Models;

namespace PartyPlanner.Api.Services.Cocktail.Services
{
    public interface ICocktailService
    {
        Data.Models.Cocktail MapCocktailDtoToCocktail(CocktailDto cocktailDto, UserIdentity user);

        Data.Models.Cocktail MapCocktailDtoToCocktail(CocktailDto cocktailDto, UserIdentity user, Data.Models.Cocktail cocktail);

    }
}
