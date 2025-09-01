using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddGRNRelatedModelsToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GoodsReceivedNote",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    GRNDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SealNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    VehicleNo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    ContainerNo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    TrailorNo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsReceivedNote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsReceivedNote_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoodsReceivedNoteProduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    GoodsReceivedNoteId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    RackLocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsReceivedNoteProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsReceivedNoteProduct_GoodsReceivedNote_GoodsReceivedNoteId",
                        column: x => x.GoodsReceivedNoteId,
                        principalTable: "GoodsReceivedNote",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoodsReceivedNoteProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoodsReceivedNoteProduct_RackLocation_RackLocationId",
                        column: x => x.RackLocationId,
                        principalTable: "RackLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivedNote_Code",
                table: "GoodsReceivedNote",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivedNote_CustomerId",
                table: "GoodsReceivedNote",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivedNoteProduct_GoodsReceivedNoteId",
                table: "GoodsReceivedNoteProduct",
                column: "GoodsReceivedNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivedNoteProduct_ProductId",
                table: "GoodsReceivedNoteProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivedNoteProduct_RackLocationId",
                table: "GoodsReceivedNoteProduct",
                column: "RackLocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoodsReceivedNoteProduct");

            migrationBuilder.DropTable(
                name: "GoodsReceivedNote");
        }
    }
}
