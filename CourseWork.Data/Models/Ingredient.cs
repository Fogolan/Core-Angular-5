using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PartyPlanner.Data.Models
{
    public class Ingredient
    {
        public Ingredient()
        {
            Recipes = new List<Recipe>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool Active { get; set; }

        public ICollection<Recipe> Recipes { get; set; }
    }
}
