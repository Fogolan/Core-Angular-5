using System.ComponentModel.DataAnnotations;

namespace PartyPlanner.Data.Models
{
    public class Role
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
