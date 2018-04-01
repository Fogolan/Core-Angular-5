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

      public DbSet<User> Users { get; set; }
    }
}
