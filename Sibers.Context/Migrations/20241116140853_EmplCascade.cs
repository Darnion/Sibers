using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sibers.Context.Migrations
{
    /// <inheritdoc />
    public partial class EmplCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProject_Employees_WorkersId",
                table: "EmployeeProject");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProject_Projects_ProjectsId",
                table: "EmployeeProject");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProject_Employees_WorkersId",
                table: "EmployeeProject",
                column: "WorkersId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProject_Projects_ProjectsId",
                table: "EmployeeProject",
                column: "ProjectsId",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProject_Employees_WorkersId",
                table: "EmployeeProject");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProject_Projects_ProjectsId",
                table: "EmployeeProject");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProject_Employees_WorkersId",
                table: "EmployeeProject",
                column: "WorkersId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProject_Projects_ProjectsId",
                table: "EmployeeProject",
                column: "ProjectsId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
