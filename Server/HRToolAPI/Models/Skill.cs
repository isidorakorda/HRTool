using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HRToolAPI.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Skill
    {
        [Key]
        public required Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string Name { get; set; }

        public ICollection<Candidate> Candidates { get; set; } = [];
    }
}
