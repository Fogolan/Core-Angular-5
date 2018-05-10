using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public int Degrees { get; set; }

        public string ImageSrc { get; set; }

        [ForeignKey("UserIdentity")]
        public string UserIdentityId { get; set; }

        public UserIdentity User { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        [Required]
        public bool Active { get; set; }

        public ICollection<Recipe> Recipes { get; set; }
    }
}
