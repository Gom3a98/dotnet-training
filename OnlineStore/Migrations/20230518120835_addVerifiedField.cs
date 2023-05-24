using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStore.Migrations
{
    /// <inheritdoc />
    public partial class addVerifiedField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isTokenVerified",
                table: "refreshtoken",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isTokenVerified",
                table: "refreshtoken");
        }
    }
}
