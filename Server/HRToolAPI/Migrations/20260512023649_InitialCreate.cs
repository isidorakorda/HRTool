using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRToolAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CandidateSkill",
                columns: table => new
                {
                    CandidatesId = table.Column<Guid>(type: "uuid", nullable: false),
                    SkillsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateSkill", x => new { x.CandidatesId, x.SkillsId });
                    table.ForeignKey(
                        name: "FK_CandidateSkill_Candidates_CandidatesId",
                        column: x => x.CandidatesId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateSkill_Skills_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Candidates",
                columns: new[] { "Id", "DateOfBirth", "Email", "Name", "Phone" },
                values: new object[,]
                {
                    { new Guid("a1111111-2222-3333-4444-555555555555"), new DateOnly(1995, 5, 20), "john@example.com", "John Doe", "123456789" },
                    { new Guid("b2222222-3333-4444-5555-666666666666"), new DateOnly(1998, 11, 2), "jane@example.com", "Jane Smith", "987654321" }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("c367a281-03d2-426d-8e19-8c2da107295d"), ".NET Core" },
                    { new Guid("d478b321-12c4-4bd4-a21e-12aa332211aa"), "English Language" },
                    { new Guid("f75853eb-42d6-4bd4-b62e-52aa88667ee3"), "Angular" }
                });

            migrationBuilder.InsertData(
                table: "CandidateSkill",
                columns: new[] { "CandidatesId", "SkillsId" },
                values: new object[,]
                {
                    { new Guid("a1111111-2222-3333-4444-555555555555"), new Guid("c367a281-03d2-426d-8e19-8c2da107295d") },
                    { new Guid("a1111111-2222-3333-4444-555555555555"), new Guid("f75853eb-42d6-4bd4-b62e-52aa88667ee3") },
                    { new Guid("b2222222-3333-4444-5555-666666666666"), new Guid("d478b321-12c4-4bd4-a21e-12aa332211aa") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_Email",
                table: "Candidates",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CandidateSkill_SkillsId",
                table: "CandidateSkill",
                column: "SkillsId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_Name",
                table: "Skills",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateSkill");

            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "Skills");
        }
    }
}
