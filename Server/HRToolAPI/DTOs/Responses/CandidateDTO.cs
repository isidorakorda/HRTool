namespace HRToolAPI.DTOs.Responses
{
    public class CandidateDTO
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required DateOnly DateOfBirth { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
        public List<SkillDTO> Skills { get; set; } = [];
    }
}
