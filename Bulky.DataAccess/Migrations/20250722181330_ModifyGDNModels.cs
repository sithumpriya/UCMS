using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ModifyGDNModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoodsDeliveryNotePickNote_PickNote_PickNoteId",
                table: "GoodsDeliveryNotePickNote");

            migrationBuilder.DropIndex(
                name: "IX_GoodsDeliveryNotePickNote_PickNoteId",
                table: "GoodsDeliveryNotePickNote");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_GoodsDeliveryNotePickNote_PickNoteId",
                table: "GoodsDeliveryNotePickNote",
                column: "PickNoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsDeliveryNotePickNote_PickNote_PickNoteId",
                table: "GoodsDeliveryNotePickNote",
                column: "PickNoteId",
                principalTable: "PickNote",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
