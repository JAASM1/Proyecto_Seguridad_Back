using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace back_sistema_de_eventos.Migrations
{
    /// <inheritdoc />
    public partial class Cascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_Events_IdEvent",
                table: "Invitations");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_Events_IdEvent",
                table: "Invitations",
                column: "IdEvent",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_Events_IdEvent",
                table: "Invitations");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_Events_IdEvent",
                table: "Invitations",
                column: "IdEvent",
                principalTable: "Events",
                principalColumn: "Id");
        }
    }
}
