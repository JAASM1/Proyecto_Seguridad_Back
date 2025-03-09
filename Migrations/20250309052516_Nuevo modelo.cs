using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace back_sistema_de_eventos.Migrations
{
    /// <inheritdoc />
    public partial class Nuevomodelo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_Users_IdUser",
                table: "Invitations");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Invitations_IdEvent_IdUser",
                table: "Invitations");

            migrationBuilder.DropIndex(
                name: "IX_Invitations_IdUser",
                table: "Invitations");

            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "Invitations");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Hour",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "InvitaAt",
                table: "Invitations",
                newName: "InvitedAt");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Events",
                newName: "IsActive");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Invitations",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "IdGuest",
                table: "Invitations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Invitations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EventDateTime",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "GuestRegistrations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdInvitation = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RegisteredAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestRegistrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuestRegistrations_Invitations_IdInvitation",
                        column: x => x.IdInvitation,
                        principalTable: "Invitations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GuestRegistrations_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_IdEvent",
                table: "Invitations",
                column: "IdEvent");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_IdGuest",
                table: "Invitations",
                column: "IdGuest");

            migrationBuilder.CreateIndex(
                name: "IX_GuestRegistrations_IdInvitation",
                table: "GuestRegistrations",
                column: "IdInvitation",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GuestRegistrations_IdUser",
                table: "GuestRegistrations",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_Users_IdGuest",
                table: "Invitations",
                column: "IdGuest",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_Users_IdGuest",
                table: "Invitations");

            migrationBuilder.DropTable(
                name: "GuestRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_Invitations_IdEvent",
                table: "Invitations");

            migrationBuilder.DropIndex(
                name: "IX_Invitations_IdGuest",
                table: "Invitations");

            migrationBuilder.DropColumn(
                name: "IdGuest",
                table: "Invitations");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Invitations");

            migrationBuilder.DropColumn(
                name: "EventDateTime",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "InvitedAt",
                table: "Invitations",
                newName: "InvitaAt");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Events",
                newName: "Status");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Invitations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "IdUser",
                table: "Invitations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Hour",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_IdEvent_IdUser",
                table: "Invitations",
                columns: new[] { "IdEvent", "IdUser" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_IdUser",
                table: "Invitations",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_Users_IdUser",
                table: "Invitations",
                column: "IdUser",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
