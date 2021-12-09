using Microsoft.EntityFrameworkCore.Migrations;

namespace RentACar.Migrations
{
    public partial class Updatedinvoiceandinvoicerule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_InvoiceRules_InvoiceRuleId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_InvoiceRuleId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "InvoiceRuleId",
                table: "Invoices");

            migrationBuilder.AddColumn<int>(
                name: "InvoiceNumber",
                table: "InvoiceRules",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceRules_InvoiceNumber",
                table: "InvoiceRules",
                column: "InvoiceNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceRules_Invoices_InvoiceNumber",
                table: "InvoiceRules",
                column: "InvoiceNumber",
                principalTable: "Invoices",
                principalColumn: "Number",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceRules_Invoices_InvoiceNumber",
                table: "InvoiceRules");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceRules_InvoiceNumber",
                table: "InvoiceRules");

            migrationBuilder.DropColumn(
                name: "InvoiceNumber",
                table: "InvoiceRules");

            migrationBuilder.AddColumn<string>(
                name: "InvoiceRuleId",
                table: "Invoices",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_InvoiceRuleId",
                table: "Invoices",
                column: "InvoiceRuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_InvoiceRules_InvoiceRuleId",
                table: "Invoices",
                column: "InvoiceRuleId",
                principalTable: "InvoiceRules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
