using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PartyPlanner.Data.Models
{
    public class Cocktail
    {
        public Cocktail()
        {
            Recipes = new List<Recipe>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Degrees { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public UserIdentity User { get; set; }

        public ICollection<Recipe> Recipes { get; set; }
    }
}
