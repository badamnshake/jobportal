using Microsoft.EntityFrameworkCore.Migrations;

namespace JobSeeker.API.Data.Migrations
{
    public partial class VacRequestOut : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VacancyRequest_JobSeekerUsers_JobSeekerUserId",
                table: "VacancyRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VacancyRequest",
                table: "VacancyRequest");

            migrationBuilder.RenameTable(
                name: "VacancyRequest",
                newName: "VacancyRequests");

            migrationBuilder.RenameIndex(
                name: "IX_VacancyRequest_JobSeekerUserId",
                table: "VacancyRequests",
                newName: "IX_VacancyRequests_JobSeekerUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VacancyRequests",
                table: "VacancyRequests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VacancyRequests_JobSeekerUsers_JobSeekerUserId",
                table: "VacancyRequests",
                column: "JobSeekerUserId",
                principalTable: "JobSeekerUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VacancyRequests_JobSeekerUsers_JobSeekerUserId",
                table: "VacancyRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VacancyRequests",
                table: "VacancyRequests");

            migrationBuilder.RenameTable(
                name: "VacancyRequests",
                newName: "VacancyRequest");

            migrationBuilder.RenameIndex(
                name: "IX_VacancyRequests_JobSeekerUserId",
                table: "VacancyRequest",
                newName: "IX_VacancyRequest_JobSeekerUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VacancyRequest",
                table: "VacancyRequest",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VacancyRequest_JobSeekerUsers_JobSeekerUserId",
                table: "VacancyRequest",
                column: "JobSeekerUserId",
                principalTable: "JobSeekerUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
