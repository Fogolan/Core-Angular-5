using Microsoft.AspNetCore.Identity;

namespace PartyPlanner.Data.Models
{
    public class User
    {
      public int Id { get; set; }
      public string IdentityId { get; set; }
      public UserIdentity Identity { get; set; }
    }
}
