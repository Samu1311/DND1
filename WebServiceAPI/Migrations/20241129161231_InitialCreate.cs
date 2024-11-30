using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebServiceAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Gender = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "MedicalData",
                columns: table => new
                {
                    MedicalDataID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<int>(type: "INTEGER", nullable: false),
                    BloodType = table.Column<string>(type: "TEXT", nullable: true),
                    Height = table.Column<float>(type: "REAL", nullable: true),
                    Weight = table.Column<float>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalData", x => x.MedicalDataID);
                    table.ForeignKey(
                        name: "FK_MedicalData_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MoleImages",
                columns: table => new
                {
                    MoleImageID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<int>(type: "INTEGER", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", nullable: false),
                    ImageData = table.Column<byte[]>(type: "BLOB", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AnalysisResults = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoleImages", x => x.MoleImageID);
                    table.ForeignKey(
                        name: "FK_MoleImages_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MRIImages",
                columns: table => new
                {
                    MRIImageID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<int>(type: "INTEGER", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", nullable: false),
                    FilePath = table.Column<string>(type: "TEXT", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AnalysisResults = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MRIImages", x => x.MRIImageID);
                    table.ForeignKey(
                        name: "FK_MRIImages_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfilePictures",
                columns: table => new
                {
                    ProfilePictureID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<int>(type: "INTEGER", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", nullable: false),
                    FilePath = table.Column<string>(type: "TEXT", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfilePictures", x => x.ProfilePictureID);
                    table.ForeignKey(
                        name: "FK_ProfilePictures_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "XrayImages",
                columns: table => new
                {
                    XrayImageID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<int>(type: "INTEGER", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", nullable: false),
                    FilePath = table.Column<string>(type: "TEXT", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AnalysisResults = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XrayImages", x => x.XrayImageID);
                    table.ForeignKey(
                        name: "FK_XrayImages_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alerts",
                columns: table => new
                {
                    AlertID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<int>(type: "INTEGER", nullable: false),
                    IsRead = table.Column<bool>(type: "INTEGER", nullable: false),
                    RelatedItemType = table.Column<string>(type: "TEXT", nullable: true),
                    MoleImageID = table.Column<int>(type: "INTEGER", nullable: true),
                    MRIImageID = table.Column<int>(type: "INTEGER", nullable: true),
                    XrayImageID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alerts", x => x.AlertID);
                    table.ForeignKey(
                        name: "FK_Alerts_MRIImages_MRIImageID",
                        column: x => x.MRIImageID,
                        principalTable: "MRIImages",
                        principalColumn: "MRIImageID");
                    table.ForeignKey(
                        name: "FK_Alerts_MoleImages_MoleImageID",
                        column: x => x.MoleImageID,
                        principalTable: "MoleImages",
                        principalColumn: "MoleImageID");
                    table.ForeignKey(
                        name: "FK_Alerts_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Alerts_XrayImages_XrayImageID",
                        column: x => x.XrayImageID,
                        principalTable: "XrayImages",
                        principalColumn: "XrayImageID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_MoleImageID",
                table: "Alerts",
                column: "MoleImageID");

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_MRIImageID",
                table: "Alerts",
                column: "MRIImageID");

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_UserID",
                table: "Alerts",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_XrayImageID",
                table: "Alerts",
                column: "XrayImageID");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalData_UserID",
                table: "MedicalData",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_MoleImages_UserID",
                table: "MoleImages",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_MRIImages_UserID",
                table: "MRIImages",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ProfilePictures_UserID",
                table: "ProfilePictures",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_XrayImages_UserID",
                table: "XrayImages",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alerts");

            migrationBuilder.DropTable(
                name: "MedicalData");

            migrationBuilder.DropTable(
                name: "ProfilePictures");

            migrationBuilder.DropTable(
                name: "MRIImages");

            migrationBuilder.DropTable(
                name: "MoleImages");

            migrationBuilder.DropTable(
                name: "XrayImages");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
