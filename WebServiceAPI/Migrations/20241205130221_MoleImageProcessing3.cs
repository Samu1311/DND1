using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebServiceAPI.Migrations
{
    /// <inheritdoc />
    public partial class MoleImageProcessing3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoleImages_Users_UserID",
                table: "MoleImages");

            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "MoleImages");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "MoleImages",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_MoleImages_UserID",
                table: "MoleImages",
                newName: "IX_MoleImages_UserId");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "MoleImages",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_MoleImages_Users_UserId",
                table: "MoleImages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoleImages_Users_UserId",
                table: "MoleImages");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "MoleImages");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "MoleImages",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_MoleImages_UserId",
                table: "MoleImages",
                newName: "IX_MoleImages_UserID");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "MoleImages",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddForeignKey(
                name: "FK_MoleImages_Users_UserID",
                table: "MoleImages",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
