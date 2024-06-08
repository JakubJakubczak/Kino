using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kino.Migrations
{
    /// <inheritdoc />
    public partial class BiletSeansNavigation2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bilet_seans_SeansIdSeansNavigationIdSeans",
                table: "bilet");

            migrationBuilder.DropIndex(
                name: "IX_bilet_SeansIdSeansNavigationIdSeans",
                table: "bilet");

            migrationBuilder.DropColumn(
                name: "SeansIdSeansNavigationIdSeans",
                table: "bilet");

            migrationBuilder.CreateIndex(
                name: "IX_bilet_SeansId_seans",
                table: "bilet",
                column: "SeansId_seans");

            migrationBuilder.AddForeignKey(
                name: "FK_bilet_seans_SeansIdSeansNavigationIdSeans",
                table: "bilet",
                column: "SeansId_seans",
                principalTable: "seans",
                principalColumn: "Id_seans");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bilet_seans_SeansIdSeansNavigationIdSeans",
                table: "bilet");

            migrationBuilder.DropIndex(
                name: "IX_bilet_SeansId_seans",
                table: "bilet");

            migrationBuilder.AddColumn<int>(
                name: "SeansIdSeansNavigationIdSeans",
                table: "bilet",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_bilet_SeansIdSeansNavigationIdSeans",
                table: "bilet",
                column: "SeansIdSeansNavigationIdSeans");

            migrationBuilder.AddForeignKey(
                name: "FK_bilet_seans_SeansIdSeansNavigationIdSeans",
                table: "bilet",
                column: "SeansIdSeansNavigationIdSeans",
                principalTable: "seans",
                principalColumn: "Id_seans");
        }
    }
}
