using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HRToolAPI.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class Candidate
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required DateOnly DateOfBirth { get; set; }

        [Required]
        [Phone]
        public required string Phone { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        public ICollection<Skill> Skills { get; set; } = [];
    }
}
