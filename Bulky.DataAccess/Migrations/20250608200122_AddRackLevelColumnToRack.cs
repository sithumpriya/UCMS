using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddRackLevelColumnToRack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RackColumn",
                table: "Rack",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RackLevel",
                table: "Rack",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RackColumn",
                table: "Rack");

            migrationBuilder.DropColumn(
                name: "RackLevel",
                table: "Rack");
        }
    }
}
