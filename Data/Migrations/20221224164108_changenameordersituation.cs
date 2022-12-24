using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class changenameordersituation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_orderSituation",
                table: "orderSituation");

            migrationBuilder.RenameTable(
                name: "orderSituation",
                newName: "OrderSituation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderSituation",
                table: "OrderSituation",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderSituation",
                table: "OrderSituation");

            migrationBuilder.RenameTable(
                name: "OrderSituation",
                newName: "orderSituation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_orderSituation",
                table: "orderSituation",
                column: "Id");
        }
    }
}
