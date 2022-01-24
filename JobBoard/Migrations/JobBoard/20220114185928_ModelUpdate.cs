using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JobBoard.Migrations.JobBoard
{
    public partial class ModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCompanyImange",
                table: "Files",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsProfileImage",
                table: "Files",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "Files",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "JobTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    JobTypeId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedByNavigationId = table.Column<int>(type: "int", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_JobTypes_JobTypeId1",
                        column: x => x.JobTypeId1,
                        principalTable: "JobTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Jobs_UserProfile_CreatedByNavigationId",
                        column: x => x.CreatedByNavigationId,
                        principalTable: "UserProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Files_JobId",
                table: "Files",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_CreatedByNavigationId",
                table: "Jobs",
                column: "CreatedByNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_JobTypeId1",
                table: "Jobs",
                column: "JobTypeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Jobs_JobId",
                table: "Files",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Jobs_JobId",
                table: "Files");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "JobTypes");

            migrationBuilder.DropIndex(
                name: "IX_Files_JobId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "IsCompanyImange",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "IsProfileImage",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "Files");
        }
    }
}
