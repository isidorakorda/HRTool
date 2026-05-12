using HRToolAPI.Data;
using HRToolAPI.DTOs.Requests;
using HRToolAPI.DTOs.Responses;
using HRToolAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HRToolAPI.Services
{
    public class SkillsService
    {
        private readonly ApplicationDbContext _context;

        public SkillsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SkillDTO>> GetAllSkills()
        {
            return await _context.Skills
                .Select(s => new SkillDTO
                {
                    Id = s.Id,
                    Name = s.Name
                })
                .ToListAsync();
        }

        public async Task CreateSkill(CreateSkillDTO dto)
        {
            bool exists = await _context.Skills.AnyAsync(s => s.Name.ToLower() == dto.Name.ToLower());
            if (exists)
            {
                throw new InvalidOperationException("There is already a skill with this name in the system");
            }
            var skill = new Skill
            {
                Id = Guid.NewGuid(),
                Name = dto.Name
            };
            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSkill(Guid id)
        {
            Skill? skill = await _context.Skills.FindAsync(id);
            if (skill == null)
            {
                throw new KeyNotFoundException("There is no skill with this id");
            }
            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();
        }
    }
}
