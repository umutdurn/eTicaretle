using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class orderreturnnew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "ReturnOrder",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ReturnOrder_OrderId",
                table: "ReturnOrder",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReturnOrder_Order_OrderId",
                table: "ReturnOrder",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReturnOrder_Order_OrderId",
                table: "ReturnOrder");

            migrationBuilder.DropIndex(
                name: "IX_ReturnOrder_OrderId",
                table: "ReturnOrder");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "ReturnOrder");
        }
    }
}
