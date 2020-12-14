using Microsoft.EntityFrameworkCore.Migrations;

namespace Soaps.Migrations
{
    public partial class AddSoapProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Soaps",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Soaps",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Price",
                table: "Soaps",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SoapTypeId",
                table: "Soaps",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SoapDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoapId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoapDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoapDetails_Soaps_SoapId",
                        column: x => x.SoapId,
                        principalTable: "Soaps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SoapTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoapTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Soaps_SoapTypeId",
                table: "Soaps",
                column: "SoapTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SoapDetails_SoapId",
                table: "SoapDetails",
                column: "SoapId");

            migrationBuilder.AddForeignKey(
                name: "FK_Soaps_SoapTypes_SoapTypeId",
                table: "Soaps",
                column: "SoapTypeId",
                principalTable: "SoapTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Soaps_SoapTypes_SoapTypeId",
                table: "Soaps");

            migrationBuilder.DropTable(
                name: "SoapDetails");

            migrationBuilder.DropTable(
                name: "SoapTypes");

            migrationBuilder.DropIndex(
                name: "IX_Soaps_SoapTypeId",
                table: "Soaps");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Soaps");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Soaps");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Soaps");

            migrationBuilder.DropColumn(
                name: "SoapTypeId",
                table: "Soaps");
        }
    }
}
