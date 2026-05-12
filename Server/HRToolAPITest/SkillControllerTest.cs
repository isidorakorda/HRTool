using HRToolAPI.Controllers;
using HRToolAPI.DTOs.Requests;
using HRToolAPI.DTOs.Responses;
using HRToolAPI.Services;
using HRToolAPI.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
namespace HRToolAPI.Tests
{
    public class SkillControllerTest
    {
        private readonly Mock<ISkillsService> _mockService;
        private readonly SkillsController _controller;

        public SkillControllerTest()
        {
            _mockService = new Mock<ISkillsService>();
            _controller = new SkillsController(_mockService.Object);
        }

        [Fact]
        public async Task GetAllSkills_WhenDataExists_ReturnsOk()
        {
            var skills = new List<SkillDTO>
            {
                new SkillDTO { Id = Guid.NewGuid(), Name = "C# Programming" }
            };
            _mockService.Setup(s => s.GetAllSkills()).ReturnsAsync(skills);

            var result = await _controller.GetAllSkills();

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetAllSkills_WhenNoData_ReturnsNoContent()
        {
            _mockService.Setup(s => s.GetAllSkills()).ReturnsAsync(new List<SkillDTO>());

            var result = await _controller.GetAllSkills();

            Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public async Task CreateSkill_WhenNameExists_ReturnsBadRequest()
        {
            var dto = new CreateSkillDTO { Name = "C#" };
            _mockService.Setup(s => s.CreateSkill(dto)).ThrowsAsync(new InvalidOperationException());

            var result = await _controller.CreateSkill(dto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DeleteSkill_OnSuccess_ReturnsNoContent()
        {
            var skillId = Guid.NewGuid();
            _mockService.Setup(s => s.DeleteSkill(skillId)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteSkill(skillId);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteSkill_WhenKeyMissing_ReturnsNotFound()
        {
            var skillId = Guid.NewGuid();
            _mockService.Setup(s => s.DeleteSkill(skillId)).ThrowsAsync(new KeyNotFoundException());

            var result = await _controller.DeleteSkill(skillId);

            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
