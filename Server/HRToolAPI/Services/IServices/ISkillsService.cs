using HRToolAPI.DTOs.Requests;
using HRToolAPI.DTOs.Responses;

namespace HRToolAPI.Services.IServices
{
    public interface ISkillsService
    {
        Task CreateSkill(CreateSkillDTO dto);
        Task DeleteSkill(Guid id);
        Task<List<SkillDTO>> GetAllSkills();
    }
}
