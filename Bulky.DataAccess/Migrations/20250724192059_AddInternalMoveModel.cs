using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddInternalMoveModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InternalMove",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousLocationId = table.Column<int>(type: "int", nullable: false),
                    PreviousLocationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewLocationId = table.Column<int>(type: "int", nullable: false),
                    NewLocationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalMove", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InternalMove");
        }
    }
}
