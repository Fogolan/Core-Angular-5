using Microsoft.EntityFrameworkCore;
using PartyPlanner.Data.Models;

namespace PartyPlanner.Data.Entities
{
    public class PartyPlannerContext: DbContext
    {
        public PartyPlannerContext(DbContextOptions<PartyPlannerContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }
    }
}
