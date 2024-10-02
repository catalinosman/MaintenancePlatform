using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaintenancePlatform.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GpsCoordinates",
                table: "MaintenanceSheets");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "MaintenanceSheets",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "MaintenanceSheets",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "MaintenanceSheets");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "MaintenanceSheets");

            migrationBuilder.AddColumn<string>(
                name: "GpsCoordinates",
                table: "MaintenanceSheets",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
