using HRToolAPI.Models;

namespace HRToolAPI.DTO
{
    public class CandidateDTO
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required DateOnly DOB { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
        public List<Skill>? skills;
    }
}
