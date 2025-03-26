using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace back_sistema_de_eventos.Migrations
{
    /// <inheritdoc />
    public partial class AddInvitationStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Invitations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Invitations");
        }
    }
}
