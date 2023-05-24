using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStore.Migrations
{
    /// <inheritdoc />
    public partial class updateField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "password",
                table: "user",
                type: "varbinary(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<byte[]>(
                name: "SaltPassword",
                table: "user",
                type: "longblob",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "user",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "SaltPassword",
                table: "user",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "longblob");
        }
    }
}
