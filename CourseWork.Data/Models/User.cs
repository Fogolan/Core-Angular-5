using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PartyPlanner.Data.Models
{
    public class User
    {
        [Required]
        [Key]
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string PasswordHash { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}
