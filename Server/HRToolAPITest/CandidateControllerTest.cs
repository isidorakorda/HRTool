using HRToolAPI.Controllers;
using HRToolAPI.DTOs.Requests;
using HRToolAPI.DTOs.Responses;
using HRToolAPI.Services;
using HRToolAPI.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace HRToolAPI.Tests
{
    public class CandidateControllerTest
    {
        private readonly Mock<ICandidatesService> _mockService;
        private readonly CandidatesController _controller;

        public CandidateControllerTest()
        {
            _mockService = new Mock<ICandidatesService>();
            _controller = new CandidatesController(_mockService.Object);
        }

        [Fact]
        public async Task GetAll_CandidatesFound_ReturnsOk()
        {
            var list = new List<CandidateDTO> { new CandidateDTO {
                Id = Guid.NewGuid(),
                Name = "Test Candidate",
                Phone = "0641234567",
                Email = "email@example.com",
                DateOfBirth = new DateOnly(2000, 1, 1),
            } };
            _mockService.Setup(s => s.GetAllCandidates(null, null)).ReturnsAsync(list);

            var result = await _controller.GetAllCandidates(null, null);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetAll_NoCandidates_ReturnsNoContent()
        {
            _mockService.Setup(s => s.GetAllCandidates(null, null)).ReturnsAsync(new List<CandidateDTO>());

            var result = await _controller.GetAllCandidates(null, null);

            Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public async Task CreateCandidate_ValidRequest_ReturnsOk()
        {
            var dto = new CreateCandidateDTO
            {
                Name = "New Guy",
                Email = "new@test.com",
                DateOfBirth = new DateOnly(2000, 1, 1),
                Phone = "0641234567",
                SkillIDs = []
            };
            _mockService.Setup(s => s.CreateCandidate(dto)).Returns(Task.CompletedTask);

            var result = await _controller.CreateCandidate(dto);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task CreateCandidate_DuplicateEmail_ReturnsBadRequest()
        {
            _mockService.Setup(s => s.CreateCandidate(It.IsAny<CreateCandidateDTO>()))
                .ThrowsAsync(new DbUpdateException());
            CreateCandidateDTO dto = new CreateCandidateDTO()
            {
                Name = "Existing User",
                DateOfBirth = new DateOnly(2000, 1, 1),
                Phone = "0641234567",
                Email = "test@mail.com",
                SkillIDs = []
            };

            var result = await _controller.CreateCandidate(dto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdateCandidate_ValidId_ReturnsOk()
        {
            var id = Guid.NewGuid();
            var skills = new List<Guid> { Guid.NewGuid() };
            _mockService.Setup(s => s.UpdateCandidate(id, skills)).Returns(Task.CompletedTask);

            var result = await _controller.UpdateCandidate(id, skills);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task UpdateCandidate_InvalidId_ReturnsNotFound()
        {
            _mockService.Setup(s => s.UpdateCandidate(It.IsAny<Guid>(), It.IsAny<List<Guid>>()))
                .ThrowsAsync(new KeyNotFoundException());

            var result = await _controller.UpdateCandidate(Guid.NewGuid(), new List<Guid>());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteCandidate_ValidId_ReturnsNoContent()
        {
            var id = Guid.NewGuid();
            _mockService.Setup(s => s.DeleteCandidate(id)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteCandidate(id);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteCandidate_InvalidId_ReturnsNotFound()
        {
            _mockService.Setup(s => s.DeleteCandidate(It.IsAny<Guid>()))
                .ThrowsAsync(new KeyNotFoundException());

            var result = await _controller.DeleteCandidate(Guid.NewGuid());

            var notFound = Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
