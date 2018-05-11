namespace PartyPlanner.Data.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        public int AmountIngredient { get; set; }

        public int IngredientId { get; set; }

        public Ingredient Ingredient { get; set; }

        public int CocktailId { get; set; }

        public Cocktail Cocktail { get; set; }
    }
}
