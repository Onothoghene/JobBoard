using Microsoft.EntityFrameworkCore.Migrations;

namespace JobBoard.Migrations.JobBoard
{
    public partial class ModelUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsCompanyImange",
                table: "Files",
                newName: "IsCompanyImage");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "UserProfile",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "UserProfile");

            migrationBuilder.RenameColumn(
                name: "IsCompanyImage",
                table: "Files",
                newName: "IsCompanyImange");
        }
    }
}
