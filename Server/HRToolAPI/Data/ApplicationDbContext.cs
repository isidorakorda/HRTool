using HRToolAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HRToolAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Skill> Skills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var skillAngularId = new Guid("f75853eb-42d6-4bd4-b62e-52aa88667ee3");
            var skillNetId = new Guid("c367a281-03d2-426d-8e19-8c2da107295d");
            var skillSqlId = new Guid("d478b321-12c4-4bd4-a21e-12aa332211aa");

            var candidateJohnId = new Guid("a1111111-2222-3333-4444-555555555555");
            var candidateJaneId = new Guid("b2222222-3333-4444-5555-666666666666");

            modelBuilder.Entity<Skill>().HasData(
                new Skill { Id = skillAngularId, Name = "Angular" },
                new Skill { Id = skillNetId, Name = ".NET Core" },
                new Skill { Id = skillSqlId, Name = "English Language" }
            );

            modelBuilder.Entity<Candidate>().HasData(
                new Candidate
                {
                    Id = candidateJohnId,
                    Name = "John Doe",
                    Email = "john@example.com",
                    Phone = "123456789",
                    DateOfBirth = new DateOnly(1995, 5, 20)
                },
                new Candidate
                {
                    Id = candidateJaneId,
                    Name = "Jane Smith",
                    Email = "jane@example.com",
                    Phone = "987654321",
                    DateOfBirth = new DateOnly(1998, 11, 2)
                }
            );

            modelBuilder.Entity("CandidateSkill").HasData(
                new { CandidatesId = candidateJohnId, SkillsId = skillAngularId },
                new { CandidatesId = candidateJohnId, SkillsId = skillNetId },
                new { CandidatesId = candidateJaneId, SkillsId = skillSqlId }
            );
        }

    }
}
