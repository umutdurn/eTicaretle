using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class atdoor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Information",
                table: "BankTransfer");

            migrationBuilder.AddColumn<string>(
                name: "PaymentAtDoorType",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentAtDoorType",
                table: "Order");

            migrationBuilder.AddColumn<string>(
                name: "Information",
                table: "BankTransfer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
