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
                    MoleImageID = table.Column<int>(type: "INTEGER", nullable: false),
                    UserID = table.Column<int>(type: "INTEGER", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", nullable: false),
                    FilePath = table.Column<string>(type: "TEXT", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AnalysisResults = table.Column<string>(type: "TEXT", nullable: true),
                    UserID1 = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoleImages", x => new { x.MoleImageID, x.UserID });
                    table.ForeignKey(
                        name: "FK_MoleImages_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoleImages_Users_UserID1",
                        column: x => x.UserID1,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "MRIImages",
                columns: table => new
                {
                    MRIImageID = table.Column<int>(type: "INTEGER", nullable: false),
                    UserID = table.Column<int>(type: "INTEGER", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", nullable: false),
                    FilePath = table.Column<string>(type: "TEXT", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AnalysisResults = table.Column<string>(type: "TEXT", nullable: true),
                    UserID1 = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MRIImages", x => new { x.MRIImageID, x.UserID });
                    table.ForeignKey(
                        name: "FK_MRIImages_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MRIImages_Users_UserID1",
                        column: x => x.UserID1,
                        principalTable: "Users",
                        principalColumn: "UserID");
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
                    XrayImageID = table.Column<int>(type: "INTEGER", nullable: false),
                    UserID = table.Column<int>(type: "INTEGER", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", nullable: false),
                    FilePath = table.Column<string>(type: "TEXT", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AnalysisResults = table.Column<string>(type: "TEXT", nullable: true),
                    UserID1 = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XrayImages", x => new { x.XrayImageID, x.UserID });
                    table.ForeignKey(
                        name: "FK_XrayImages_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_XrayImages_Users_UserID1",
                        column: x => x.UserID1,
                        principalTable: "Users",
                        principalColumn: "UserID");
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
                    XrayImageID = table.Column<int>(type: "INTEGER", nullable: true),
                    UserID1 = table.Column<int>(type: "INTEGER", nullable: true),
                    MoleImageID1 = table.Column<int>(type: "INTEGER", nullable: true),
                    MoleImageUserID = table.Column<int>(type: "INTEGER", nullable: true),
                    MRIImageID1 = table.Column<int>(type: "INTEGER", nullable: true),
                    MRIImageUserID = table.Column<int>(type: "INTEGER", nullable: true),
                    XrayImageID1 = table.Column<int>(type: "INTEGER", nullable: true),
                    XrayImageUserID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alerts", x => x.AlertID);
                    table.ForeignKey(
                        name: "FK_Alerts_MRIImages_MRIImageID1_MRIImageUserID",
                        columns: x => new { x.MRIImageID1, x.MRIImageUserID },
                        principalTable: "MRIImages",
                        principalColumns: new[] { "MRIImageID", "UserID" });
                    table.ForeignKey(
                        name: "FK_Alerts_MoleImages_MoleImageID1_MoleImageUserID",
                        columns: x => new { x.MoleImageID1, x.MoleImageUserID },
                        principalTable: "MoleImages",
                        principalColumns: new[] { "MoleImageID", "UserID" });
                    table.ForeignKey(
                        name: "FK_Alerts_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Alerts_Users_UserID1",
                        column: x => x.UserID1,
                        principalTable: "Users",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK_Alerts_XrayImages_XrayImageID1_XrayImageUserID",
                        columns: x => new { x.XrayImageID1, x.XrayImageUserID },
                        principalTable: "XrayImages",
                        principalColumns: new[] { "XrayImageID", "UserID" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_MoleImageID1_MoleImageUserID",
                table: "Alerts",
                columns: new[] { "MoleImageID1", "MoleImageUserID" });

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_MRIImageID1_MRIImageUserID",
                table: "Alerts",
                columns: new[] { "MRIImageID1", "MRIImageUserID" });

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_UserID",
                table: "Alerts",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_UserID1",
                table: "Alerts",
                column: "UserID1");

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_XrayImageID1_XrayImageUserID",
                table: "Alerts",
                columns: new[] { "XrayImageID1", "XrayImageUserID" });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalData_UserID",
                table: "MedicalData",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_MoleImages_UserID",
                table: "MoleImages",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_MoleImages_UserID1",
                table: "MoleImages",
                column: "UserID1");

            migrationBuilder.CreateIndex(
                name: "IX_MRIImages_UserID",
                table: "MRIImages",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_MRIImages_UserID1",
                table: "MRIImages",
                column: "UserID1");

            migrationBuilder.CreateIndex(
                name: "IX_ProfilePictures_UserID",
                table: "ProfilePictures",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_XrayImages_UserID",
                table: "XrayImages",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_XrayImages_UserID1",
                table: "XrayImages",
                column: "UserID1");
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
