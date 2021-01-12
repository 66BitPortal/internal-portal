using Microsoft.EntityFrameworkCore.Migrations;

namespace _66bitProject.Migrations
{
    public partial class BonusTableNameChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bonus_Users_EmployeeId",
                table: "Bonus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bonus",
                table: "Bonus");

            migrationBuilder.RenameTable(
                name: "Bonus",
                newName: "Bonuses");

            migrationBuilder.RenameIndex(
                name: "IX_Bonus_EmployeeId",
                table: "Bonuses",
                newName: "IX_Bonuses_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bonuses",
                table: "Bonuses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bonuses_Users_EmployeeId",
                table: "Bonuses",
                column: "EmployeeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bonuses_Users_EmployeeId",
                table: "Bonuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bonuses",
                table: "Bonuses");

            migrationBuilder.RenameTable(
                name: "Bonuses",
                newName: "Bonus");

            migrationBuilder.RenameIndex(
                name: "IX_Bonuses_EmployeeId",
                table: "Bonus",
                newName: "IX_Bonus_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bonus",
                table: "Bonus",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bonus_Users_EmployeeId",
                table: "Bonus",
                column: "EmployeeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
