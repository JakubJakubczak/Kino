using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kino.Migrations
{
    /// <inheritdoc />
    public partial class BiletSeansNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
