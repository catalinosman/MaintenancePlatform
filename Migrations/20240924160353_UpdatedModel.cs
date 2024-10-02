using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaintenancePlatform.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GPSCoordinates",
                table: "MaintenanceSheets",
                newName: "GpsCoordinates");

            migrationBuilder.AddColumn<string>(
                name: "UploadedFileName",
                table: "MaintenanceSheets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UploadedFilePath",
                table: "MaintenanceSheets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UploadedFileName",
                table: "MaintenanceSheets");

            migrationBuilder.DropColumn(
                name: "UploadedFilePath",
                table: "MaintenanceSheets");

            migrationBuilder.RenameColumn(
                name: "GpsCoordinates",
                table: "MaintenanceSheets",
                newName: "GPSCoordinates");
        }
    }
}
