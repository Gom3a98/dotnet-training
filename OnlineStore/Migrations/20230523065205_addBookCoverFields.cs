using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStore.Migrations
{
    /// <inheritdoc />
    public partial class addBookCoverFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BackCover",
                table: "book",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "FrontCover",
                table: "book",
                type: "longtext",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackCover",
                table: "book");

            migrationBuilder.DropColumn(
                name: "FrontCover",
                table: "book");
        }
    }
}
