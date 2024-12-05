using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebServiceAPI.Migrations
{
    /// <inheritdoc />
    public partial class MedicalData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalData_Users_UserID",
                table: "MedicalData");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "MedicalData",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "BloodType",
                table: "MedicalData",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Alcohol",
                table: "MedicalData",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "DairyAllergy",
                table: "MedicalData",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "GlutenAllergy",
                table: "MedicalData",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OtherAllergies",
                table: "MedicalData",
                type: "TEXT",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PeanutsAllergy",
                table: "MedicalData",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PollenAllergy",
                table: "MedicalData",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ShellfishAllergy",
                table: "MedicalData",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Smoking",
                table: "MedicalData",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalData_Users_UserID",
                table: "MedicalData",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalData_Users_UserID",
                table: "MedicalData");

            migrationBuilder.DropColumn(
                name: "Alcohol",
                table: "MedicalData");

            migrationBuilder.DropColumn(
                name: "DairyAllergy",
                table: "MedicalData");

            migrationBuilder.DropColumn(
                name: "GlutenAllergy",
                table: "MedicalData");

            migrationBuilder.DropColumn(
                name: "OtherAllergies",
                table: "MedicalData");

            migrationBuilder.DropColumn(
                name: "PeanutsAllergy",
                table: "MedicalData");

            migrationBuilder.DropColumn(
                name: "PollenAllergy",
                table: "MedicalData");

            migrationBuilder.DropColumn(
                name: "ShellfishAllergy",
                table: "MedicalData");

            migrationBuilder.DropColumn(
                name: "Smoking",
                table: "MedicalData");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "MedicalData",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BloodType",
                table: "MedicalData",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalData_Users_UserID",
                table: "MedicalData",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
