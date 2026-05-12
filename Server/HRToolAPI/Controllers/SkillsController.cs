using HRToolAPI.DTOs.Requests;
using HRToolAPI.DTOs.Responses;
using HRToolAPI.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace HRToolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillsService _skillsService;

        public SkillsController(ISkillsService skillsService)
        {
            _skillsService = skillsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<SkillDTO>>> GetAllSkills()
        {
            try
            {
                List<SkillDTO> skills = await _skillsService.GetAllSkills();
                if (skills == null || skills.Count == 0)
                {
                    return NoContent();
                }
                return Ok(skills);
            }
            catch (Exception)
            {
                return StatusCode(500, "An Internal Server Error Has Occured");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSkill([FromBody] CreateSkillDTO dto)
        {
            try
            {
                await _skillsService.CreateSkill(dto);
                return Created();
            }
            catch (InvalidOperationException)
            {
                return BadRequest("There is already a skill with this name");
            }
            catch (Exception)
            {
                return StatusCode(500, "An Internal Server Error Has Occured");
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteSkill(Guid id)
        {
            try
            {
                await _skillsService.DeleteSkill(id);
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
