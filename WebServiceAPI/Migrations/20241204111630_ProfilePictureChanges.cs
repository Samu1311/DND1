using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebServiceAPI.Migrations
{
    /// <inheritdoc />
    public partial class ProfilePictureChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ThumbnailPath",
                table: "ProfilePictures",
                newName: "Url");

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailUrl",
                table: "ProfilePictures",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbnailUrl",
                table: "ProfilePictures");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "ProfilePictures",
                newName: "ThumbnailPath");
        }
    }
}
