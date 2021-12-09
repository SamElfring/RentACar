using Microsoft.EntityFrameworkCore.Migrations;

namespace RentACar.Migrations
{
    public partial class Removedlicenseplatefrominvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Cars_CarLicensePlate",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_CarLicensePlate",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "CarLicensePlate",
                table: "Invoices");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CarLicensePlate",
                table: "Invoices",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CarLicensePlate",
                table: "Invoices",
                column: "CarLicensePlate");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Cars_CarLicensePlate",
                table: "Invoices",
                column: "CarLicensePlate",
                principalTable: "Cars",
                principalColumn: "LicensePlate",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
