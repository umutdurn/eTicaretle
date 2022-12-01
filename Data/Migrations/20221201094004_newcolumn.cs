using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class newcolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ScreenId",
                table: "HomeColumns",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "columnDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HomeColumnId = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Background = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FrontText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Column = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FrontTitle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_columnDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_columnDetail_HomeColumns_HomeColumnId",
                        column: x => x.HomeColumnId,
                        principalTable: "HomeColumns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Screen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Resolution = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screen", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HomeColumns_ScreenId",
                table: "HomeColumns",
                column: "ScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_columnDetail_HomeColumnId",
                table: "columnDetail",
                column: "HomeColumnId");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeColumns_Screen_ScreenId",
                table: "HomeColumns",
                column: "ScreenId",
                principalTable: "Screen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeColumns_Screen_ScreenId",
                table: "HomeColumns");

            migrationBuilder.DropTable(
                name: "columnDetail");

            migrationBuilder.DropTable(
                name: "Screen");

            migrationBuilder.DropIndex(
                name: "IX_HomeColumns_ScreenId",
                table: "HomeColumns");

            migrationBuilder.DropColumn(
                name: "ScreenId",
                table: "HomeColumns");
        }
    }
}
