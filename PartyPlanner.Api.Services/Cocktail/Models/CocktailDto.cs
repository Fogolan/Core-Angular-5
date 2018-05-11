using System;
using System.Collections.Generic;
using PartyPlanner.Api.Services.Ingredient.Models;

namespace PartyPlanner.Api.Services.Cocktail.Models
{
    public class CocktailDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Degrees { get; set; }

        public int Amount { get; set; }

        public string ImageSrc { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string UserId { get; set; }

        public IEnumerable<IngredientDto> Ingredients { get; set; }
    }
}
