using Microsoft.EntityFrameworkCore.Migrations;

namespace Employer.API.Data.Migrations
{
    public partial class smallChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmailUser",
                table: "Vacancies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByEmailUser",
                table: "Vacancies");
        }
    }
}
