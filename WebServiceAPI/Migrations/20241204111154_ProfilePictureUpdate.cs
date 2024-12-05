using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebServiceAPI.Migrations
{
    /// <inheritdoc />
    public partial class ProfilePictureUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThumbnailPath",
                table: "ProfilePictures",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbnailPath",
                table: "ProfilePictures");
        }
    }
}
