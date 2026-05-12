using System.ComponentModel.DataAnnotations;

namespace HRToolAPI.DTOs.Requests
{
    public class CreateCandidateDTO
    {
        [Required]
        [StringLength(100)]
        public required string Name { get; set; }

        [Required]
        public required DateOnly DateOfBirth { get; set; }

        [Phone]
        public required string Phone { get; set; }

        [EmailAddress]
        public required string Email { get; set; }

        public required List<Guid> SkillIDs { get; set; } = [];
    }
}
