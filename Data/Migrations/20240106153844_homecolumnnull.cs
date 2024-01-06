using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class homecolumnnull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_columnDetail_HomeColumns_HomeColumnId",
                table: "columnDetail");

            migrationBuilder.AlterColumn<int>(
                name: "HomeColumnId",
                table: "columnDetail",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_columnDetail_HomeColumns_HomeColumnId",
                table: "columnDetail",
                column: "HomeColumnId",
                principalTable: "HomeColumns",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_columnDetail_HomeColumns_HomeColumnId",
                table: "columnDetail");

            migrationBuilder.AlterColumn<int>(
                name: "HomeColumnId",
                table: "columnDetail",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_columnDetail_HomeColumns_HomeColumnId",
                table: "columnDetail",
                column: "HomeColumnId",
                principalTable: "HomeColumns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
