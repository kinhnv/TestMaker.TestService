using Microsoft.EntityFrameworkCore.Migrations;

namespace TestMaker.TestService.Infrastructure.Migrations
{
    public partial class AddIsDeletedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Tests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Sections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Questions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Questions");
        }
    }
}
