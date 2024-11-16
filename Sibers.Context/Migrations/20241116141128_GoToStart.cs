using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sibers.Context.Migrations
{
    /// <inheritdoc />
    public partial class GoToStart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProject_Employees_WorkersId",
                table: "EmployeeProject");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProject_Employees_WorkersId",
                table: "EmployeeProject",
                column: "WorkersId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProject_Employees_WorkersId",
                table: "EmployeeProject");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProject_Employees_WorkersId",
                table: "EmployeeProject",
                column: "WorkersId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
