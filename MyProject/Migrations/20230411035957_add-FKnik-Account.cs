using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyProject.Migrations
{
    /// <inheritdoc />
    public partial class addFKnikAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_Account_TB_Employee_EmployeeNIK",
                table: "TB_Account");

            migrationBuilder.DropIndex(
                name: "IX_TB_Account_EmployeeNIK",
                table: "TB_Account");

            migrationBuilder.DropColumn(
                name: "EmployeeNIK",
                table: "TB_Account");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_Account_TB_Employee_NIK",
                table: "TB_Account",
                column: "NIK",
                principalTable: "TB_Employee",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_Account_TB_Employee_NIK",
                table: "TB_Account");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeNIK",
                table: "TB_Account",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_Account_EmployeeNIK",
                table: "TB_Account",
                column: "EmployeeNIK");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_Account_TB_Employee_EmployeeNIK",
                table: "TB_Account",
                column: "EmployeeNIK",
                principalTable: "TB_Employee",
                principalColumn: "NIK");
        }
    }
}
