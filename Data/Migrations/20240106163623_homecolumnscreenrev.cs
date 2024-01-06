using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class homecolumnscreenrev : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeColumns_Screen_ScreenId",
                table: "HomeColumns");

            migrationBuilder.AlterColumn<int>(
                name: "ScreenId",
                table: "HomeColumns",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeColumns_Screen_ScreenId",
                table: "HomeColumns",
                column: "ScreenId",
                principalTable: "Screen",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeColumns_Screen_ScreenId",
                table: "HomeColumns");

            migrationBuilder.AlterColumn<int>(
                name: "ScreenId",
                table: "HomeColumns",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HomeColumns_Screen_ScreenId",
                table: "HomeColumns",
                column: "ScreenId",
                principalTable: "Screen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
