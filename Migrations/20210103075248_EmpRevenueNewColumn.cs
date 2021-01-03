using Microsoft.EntityFrameworkCore.Migrations;

namespace _66bitProject.Migrations
{
    public partial class EmpRevenueNewColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "EmployeeRevenues",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "EmployeeRevenues");
        }
    }
}
