using Microsoft.EntityFrameworkCore.Migrations;

namespace _66bitProject.Migrations
{
    public partial class OverworkUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CalculatedPayment",
                table: "Overworks",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalculatedPayment",
                table: "Overworks");
        }
    }
}
