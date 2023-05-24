using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace OnlineStore.Migrations
{
    /// <inheritdoc />
    public partial class setup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bookauthor");

            migrationBuilder.DropTable(
                name: "job");

            migrationBuilder.DropTable(
                name: "refreshtoken");

            migrationBuilder.DropTable(
                name: "sale");

            migrationBuilder.DropTable(
                name: "author");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "book");

            migrationBuilder.DropTable(
                name: "store");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "publisher");
        }
    }
}
