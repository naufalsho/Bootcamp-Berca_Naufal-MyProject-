using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyProject.Migrations
{
    /// <inheritdoc />
    public partial class addTableAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_Account",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeNIK = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Account", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_TB_Account_TB_Employee_EmployeeNIK",
                        column: x => x.EmployeeNIK,
                        principalTable: "TB_Employee",
                        principalColumn: "NIK");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_Account_EmployeeNIK",
                table: "TB_Account",
                column: "EmployeeNIK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_Account");
        }
    }
}
