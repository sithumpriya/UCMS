using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddPickNoteRelatedModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PickNote",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PNDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PickNote", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PickNoteRackLocation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RackLocationId = table.Column<int>(type: "int", nullable: false),
                    RequestQuantity = table.Column<int>(type: "int", nullable: false),
                    PickNoteId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PickNoteRackLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PickNoteRackLocation_PickNote_PickNoteId",
                        column: x => x.PickNoteId,
                        principalTable: "PickNote",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PickNote_Code",
                table: "PickNote",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PickNoteRackLocation_PickNoteId",
                table: "PickNoteRackLocation",
                column: "PickNoteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PickNoteRackLocation");

            migrationBuilder.DropTable(
                name: "PickNote");
        }
    }
}
