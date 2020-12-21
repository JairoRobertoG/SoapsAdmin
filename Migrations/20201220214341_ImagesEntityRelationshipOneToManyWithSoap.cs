using Microsoft.EntityFrameworkCore.Migrations;

namespace Soaps.Migrations
{
    public partial class ImagesEntityRelationshipOneToManyWithSoap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageSoap");

            migrationBuilder.AddColumn<int>(
                name: "SoapId",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Images_SoapId",
                table: "Images",
                column: "SoapId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Soaps_SoapId",
                table: "Images",
                column: "SoapId",
                principalTable: "Soaps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Soaps_SoapId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_SoapId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "SoapId",
                table: "Images");

            migrationBuilder.CreateTable(
                name: "ImageSoap",
                columns: table => new
                {
                    ImagesId = table.Column<int>(type: "int", nullable: false),
                    SoapsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageSoap", x => new { x.ImagesId, x.SoapsId });
                    table.ForeignKey(
                        name: "FK_ImageSoap_Images_ImagesId",
                        column: x => x.ImagesId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImageSoap_Soaps_SoapsId",
                        column: x => x.SoapsId,
                        principalTable: "Soaps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImageSoap_SoapsId",
                table: "ImageSoap",
                column: "SoapsId");
        }
    }
}
