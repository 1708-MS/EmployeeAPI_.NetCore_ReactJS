using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeAPI.Migrations
{
    public partial class ChangeNameOfDesignationCodeInDesignationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DesignationDsgCode",
                table: "Designations",
                newName: "DesignationCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DesignationCode",
                table: "Designations",
                newName: "DesignationDsgCode");
        }
    }
}
