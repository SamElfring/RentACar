using Microsoft.EntityFrameworkCore.Migrations;

namespace RentACar.Migrations
{
    public partial class Addedclassestocars : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Class",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Class",
                table: "Cars");
        }
    }
}
