using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class resolution : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Resolution",
                table: "Screen",
                newName: "MinResolution");

            migrationBuilder.AddColumn<int>(
                name: "MaxResolution",
                table: "Screen",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxResolution",
                table: "Screen");

            migrationBuilder.RenameColumn(
                name: "MinResolution",
                table: "Screen",
                newName: "Resolution");
        }
    }
}
