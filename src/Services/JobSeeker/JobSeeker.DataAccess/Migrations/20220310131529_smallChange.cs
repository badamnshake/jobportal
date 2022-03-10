using Microsoft.EntityFrameworkCore.Migrations;

namespace JobSeeker.DataAccess.Migrations
{
    public partial class smallChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JobSeekerUsers_Email",
                table: "JobSeekerUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "JobSeekerUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "JobSeekerUsers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekerUsers_Email",
                table: "JobSeekerUsers",
                column: "Email",
                unique: true);
        }
    }
}
