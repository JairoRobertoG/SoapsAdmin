using Microsoft.EntityFrameworkCore.Migrations;

namespace Soaps.Migrations
{
    public partial class RemoveImagePropertySoap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Soaps");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Soaps",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
