using Microsoft.EntityFrameworkCore.Migrations;

namespace JobSeeker.API.Data.Migrations
{
    public partial class SimpleUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JobSeekerUsers_Email_AppUserEmail",
                table: "JobSeekerUsers");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JobSeekerUsers_AppUserEmail",
                table: "JobSeekerUsers");

            migrationBuilder.DropIndex(
                name: "IX_JobSeekerUsers_Email",
                table: "JobSeekerUsers");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekerUsers_Email_AppUserEmail",
                table: "JobSeekerUsers",
                columns: new[] { "Email", "AppUserEmail" },
                unique: true);
        }
    }
}
