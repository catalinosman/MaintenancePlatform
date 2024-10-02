using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaintenancePlatform.Migrations
{
    /// <inheritdoc />
    public partial class DisplayLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LocationDescription",
                table: "MaintenanceSheets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationDescription",
                table: "MaintenanceSheets");
        }
    }
}
