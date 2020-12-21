using Microsoft.EntityFrameworkCore.Migrations;

namespace Soaps.Migrations
{
    public partial class ImagesEntityRelationshipWithSoap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageSoap");
        }
    }
}
