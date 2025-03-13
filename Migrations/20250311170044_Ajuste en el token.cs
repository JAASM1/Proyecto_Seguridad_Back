using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace back_sistema_de_eventos.Migrations
{
    /// <inheritdoc />
    public partial class Ajusteeneltoken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Events",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Events_Token",
                table: "Events",
                column: "Token",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Events_Token",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Events");
        }
    }
}
