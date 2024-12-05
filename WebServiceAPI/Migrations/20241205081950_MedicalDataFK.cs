using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebServiceAPI.Migrations
{
    /// <inheritdoc />
    public partial class MedicalDataFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalData_Users_UserID",
                table: "MedicalData");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "MedicalData",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicalData_UserID",
                table: "MedicalData",
                newName: "IX_MedicalData_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "MedicalData",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalData_Users_UserId",
                table: "MedicalData",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalData_Users_UserId",
                table: "MedicalData");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "MedicalData",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_MedicalData_UserId",
                table: "MedicalData",
                newName: "IX_MedicalData_UserID");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "MedicalData",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalData_Users_UserID",
                table: "MedicalData",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID");
        }
    }
}
