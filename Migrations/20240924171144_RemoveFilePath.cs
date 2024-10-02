using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaintenancePlatform.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFilePath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UploadedFilePath",
                table: "MaintenanceSheets");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UploadedFilePath",
                table: "MaintenanceSheets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
