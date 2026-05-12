using HRToolAPI.Data;
using HRToolAPI.DTOs.Requests;
using HRToolAPI.DTOs.Responses;
using HRToolAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HRToolAPI.Services
{
    public class CandidatesService
    {
        private readonly ApplicationDbContext _context;
        public CandidatesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CandidateDTO>> GetAll(string? name, List<Guid>? skillIds)
        {
            var query = _context.Candidates.AsQueryable();
            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(c => c.Name.ToLower().Contains(name.ToLower()));
            if (skillIds != null && skillIds.Any())
                query = query.Where(c => skillIds.All(sid => c.Skills.Any(s => s.Id == sid)));
            return await query
               .Select(c => new CandidateDTO
               {
                   Id = c.Id,
                   Name = c.Name,
                   DateOfBirth = c.DateOfBirth,
                   Phone = c.Phone,
                   Email = c.Email,
                   Skills = c.Skills.Select(s => new SkillDTO
                   {
                       Id = s.Id,
                       Name = s.Name
                   })
                   .ToList()
               })
               .ToListAsync();
        }

        public async Task CreateCandidate(CreateCandidateDTO dto)
        {
            Candidate candidate = new Candidate
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                DateOfBirth = dto.DateOfBirth,
                Phone = dto.Phone,
                Email = dto.Email
            };
            if (dto.SkillIDs.Count > 0)
            {
                List<Skill> skills = await _context.Skills
                    .Where(s => dto.SkillIDs.Contains(s.Id))
                    .ToListAsync();

                candidate.Skills = skills;
            }
            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCandidate(Guid id, List<Guid> skillIds)
        {
            Candidate? candidate = await _context.Candidates
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (candidate == null)
                throw new KeyNotFoundException();
            List<Skill> skills = await _context.Skills
                    .Where(s => skillIds.Contains(s.Id))
                    .ToListAsync();

            candidate.Skills = skills;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCandidate(Guid id)
        {
            Candidate? candidate = await _context.Candidates.FindAsync(id);
            if (candidate == null)
                throw new KeyNotFoundException();

            _context.Candidates.Remove(candidate);
            await _context.SaveChangesAsync();
        }
    }
}
