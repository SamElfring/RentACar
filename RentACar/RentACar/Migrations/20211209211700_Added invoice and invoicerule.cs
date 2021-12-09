using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RentACar.Migrations
{
    public partial class Addedinvoiceandinvoicerule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvoiceRules",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CarLicensePlate = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceRules_Cars_CarLicensePlate",
                        column: x => x.CarLicensePlate,
                        principalTable: "Cars",
                        principalColumn: "LicensePlate",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Number = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceRuleId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarLicensePlate = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Number);
                    table.ForeignKey(
                        name: "FK_Invoices_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_AspNetUsers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_Cars_CarLicensePlate",
                        column: x => x.CarLicensePlate,
                        principalTable: "Cars",
                        principalColumn: "LicensePlate",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_InvoiceRules_InvoiceRuleId",
                        column: x => x.InvoiceRuleId,
                        principalTable: "InvoiceRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceRules_CarLicensePlate",
                table: "InvoiceRules",
                column: "CarLicensePlate");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CarLicensePlate",
                table: "Invoices",
                column: "CarLicensePlate");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CustomerId",
                table: "Invoices",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_EmployeeId",
                table: "Invoices",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_InvoiceRuleId",
                table: "Invoices",
                column: "InvoiceRuleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "InvoiceRules");
        }
    }
}
