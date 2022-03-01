using Microsoft.EntityFrameworkCore.Migrations;

namespace JobSeeker.API.Data.Migrations
{
    public partial class SimplechangeofUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "JobSeekerUsers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserEmail",
                table: "JobSeekerUsers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekerUsers_Email_AppUserEmail",
                table: "JobSeekerUsers",
                columns: new[] { "Email", "AppUserEmail" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JobSeekerUsers_Email_AppUserEmail",
                table: "JobSeekerUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "JobSeekerUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserEmail",
                table: "JobSeekerUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
