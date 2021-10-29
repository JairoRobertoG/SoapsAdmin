using Microsoft.EntityFrameworkCore.Migrations;

namespace Soaps.Migrations
{
    public partial class QuantitySoap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Soaps_SoapTypes_SoapTypeId",
                table: "Soaps");

            migrationBuilder.AlterColumn<int>(
                name: "SoapTypeId",
                table: "Soaps",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Soaps",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Soaps");

            migrationBuilder.AlterColumn<int>(
                name: "SoapTypeId",
                table: "Soaps",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Soaps_SoapTypes_SoapTypeId",
                table: "Soaps",
                column: "SoapTypeId",
                principalTable: "SoapTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
