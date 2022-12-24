using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class orderaddsitu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderSituationId",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Order_OrderSituationId",
                table: "Order",
                column: "OrderSituationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_OrderSituation_OrderSituationId",
                table: "Order",
                column: "OrderSituationId",
                principalTable: "OrderSituation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_OrderSituation_OrderSituationId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_OrderSituationId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "OrderSituationId",
                table: "Order");
        }
    }
}
