using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebServiceAPI.Migrations
{
    /// <inheritdoc />
    public partial class ElizasUpdates1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "FileContent",
                table: "MRIImages",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileContent",
                table: "MRIImages");
        }
    }
}
