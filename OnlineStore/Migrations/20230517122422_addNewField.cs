using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStore.Migrations
{
    /// <inheritdoc />
    public partial class addNewField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SaltPassword",
                table: "user",
                type: "longtext",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaltPassword",
                table: "user");
        }
    }
}
