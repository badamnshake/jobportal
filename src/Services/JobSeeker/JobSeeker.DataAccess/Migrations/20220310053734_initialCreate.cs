using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JobSeeker.DataAccess.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobSeekerUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AppUserEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TotalExperience = table.Column<int>(type: "int", nullable: false),
                    ExpectedSalaryAnnual = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "Date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSeekerUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Experiences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobSeekerUserId = table.Column<int>(type: "int", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    StartDate = table.Column<DateTime>(type: "Date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "Date", nullable: false),
                    CompanyUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Designation = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    JobDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experiences_JobSeekerUsers_JobSeekerUserId",
                        column: x => x.JobSeekerUserId,
                        principalTable: "JobSeekerUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Qualifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobSeekerUserId = table.Column<int>(type: "int", nullable: false),
                    QualificationName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    University = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfCompletion = table.Column<DateTime>(type: "Date", nullable: false),
                    GradeOrScore = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Qualifications_JobSeekerUsers_JobSeekerUserId",
                        column: x => x.JobSeekerUserId,
                        principalTable: "JobSeekerUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VacancyRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VacancyId = table.Column<int>(type: "int", nullable: false),
                    JobSeekerUserId = table.Column<int>(type: "int", nullable: false),
                    AppliedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacancyRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VacancyRequests_JobSeekerUsers_JobSeekerUserId",
                        column: x => x.JobSeekerUserId,
                        principalTable: "JobSeekerUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_JobSeekerUserId",
                table: "Experiences",
                column: "JobSeekerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekerUsers_AppUserEmail",
                table: "JobSeekerUsers",
                column: "AppUserEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekerUsers_Email",
                table: "JobSeekerUsers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Qualifications_JobSeekerUserId",
                table: "Qualifications",
                column: "JobSeekerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_VacancyRequests_JobSeekerUserId",
                table: "VacancyRequests",
                column: "JobSeekerUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Experiences");

            migrationBuilder.DropTable(
                name: "Qualifications");

            migrationBuilder.DropTable(
                name: "VacancyRequests");

            migrationBuilder.DropTable(
                name: "JobSeekerUsers");
        }
    }
}
