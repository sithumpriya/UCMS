using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddProductToRackLocationModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvailableQuantity",
                table: "RackLocation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GoodsReceivedNoteProductId",
                table: "RackLocation",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableQuantity",
                table: "RackLocation");

            migrationBuilder.DropColumn(
                name: "GoodsReceivedNoteProductId",
                table: "RackLocation");
        }
    }
}
