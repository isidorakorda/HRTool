using HRToolAPI.DTOs.Requests;
using HRToolAPI.DTOs.Responses;

namespace HRToolAPI.Services.IServices
{
    public interface ICandidatesService
    {
        Task CreateCandidate(CreateCandidateDTO dto);
        Task DeleteCandidate(Guid id);
        Task<List<CandidateDTO>> GetAllCandidates(string? name, List<Guid>? skillIds);
        Task UpdateCandidate(Guid id, List<Guid> skillIds);
    }
}
