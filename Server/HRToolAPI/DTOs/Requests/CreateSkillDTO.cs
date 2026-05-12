using System.ComponentModel.DataAnnotations;

namespace HRToolAPI.DTOs.Requests
{
    public class CreateSkillDTO
    {
        [Required]
        [StringLength(100)]
        public required string Name { get; set; }
    }
}
