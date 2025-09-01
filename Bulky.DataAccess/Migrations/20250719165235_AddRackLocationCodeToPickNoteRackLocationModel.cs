using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddRackLocationCodeToPickNoteRackLocationModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RackLocationCode",
                table: "PickNoteRackLocation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RackLocationCode",
                table: "PickNoteRackLocation");
        }
    }
}
