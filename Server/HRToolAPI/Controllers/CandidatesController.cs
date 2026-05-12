using HRToolAPI.DTOs.Requests;
using HRToolAPI.DTOs.Responses;
using HRToolAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRToolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private readonly CandidatesService _candidateService;
        public CandidatesController(CandidatesService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CandidateDTO>>> GetAll([FromQuery] string? name, [FromQuery] List<Guid>? skillIds)
        {
            try
            {
                List<CandidateDTO> candidates = await _candidateService.GetAll(name, skillIds);
                if (candidates == null || candidates.Count == 0)
                {
                    return NoContent();
                }
                return Ok(candidates);
            }
            catch (Exception)
            {
                return StatusCode(500, "An Internal Server Has Occured");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCandidate([FromBody] CreateCandidateDTO dto)
        {
            try
            {
                await _candidateService.CreateCandidate(dto);
                return Ok();
            }
            catch (DbUpdateException)
            {
                return BadRequest("There is already a candidate with this Email Address");
            }
            catch (Exception)
            {
                return StatusCode(500, "An Internal Server Error Has Occured");
            }

        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCandidate(Guid id, [FromQuery] List<Guid> skillIds)
        {
            try
            {
                await _candidateService.UpdateCandidate(id, skillIds);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCandidate(Guid id)
        {
            try
            {
                await _candidateService.DeleteCandidate(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("There is no candidate with this id");
            }
            catch (Exception)
            {
                return StatusCode(500, "An Internal Server Error Has Occured");
            }
        }
    }
}
