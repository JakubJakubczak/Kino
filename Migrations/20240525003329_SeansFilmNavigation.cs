using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kino.Migrations
{
    /// <inheritdoc />
    public partial class SeansFilmNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bilet");

            migrationBuilder.DropTable(
                name: "karnet");

            migrationBuilder.DropTable(
                name: "karnet_seans");

            migrationBuilder.DropTable(
                name: "pracownik");

            migrationBuilder.DropTable(
                name: "sala");

            migrationBuilder.DropTable(
                name: "seans");

            migrationBuilder.DropTable(
                name: "klient");

            migrationBuilder.DropTable(
                name: "film");
        }
    }
}
