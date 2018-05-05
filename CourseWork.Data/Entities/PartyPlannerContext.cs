using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PartyPlanner.Data.Models;

namespace PartyPlanner.Data.Entities
{
    public class PartyPlannerContext : IdentityDbContext<UserIdentity>
    {
      public PartyPlannerContext(DbContextOptions<PartyPlannerContext> options)
        : base(options)
      {
      }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Recipe>()
                .HasKey(ct => new { ct.CocktailId, ct.IngredientId });

            modelBuilder.Entity<Recipe>()
                .HasOne(ct => ct.Cocktail)
                .WithMany(c => c.Recipes)
                .HasForeignKey(ct => ct.CocktailId);

            modelBuilder.Entity<Recipe>()
                .HasOne(ct => ct.Ingredient)
                .WithMany(t => t.Recipes)
                .HasForeignKey(ct => ct.IngredientId);

        }


        public DbSet<Cocktail> Cocktails { get; set; }

        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }


        public DbSet<User> Users { get; set; }
    }
}
