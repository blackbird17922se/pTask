using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pTask.Migrations
{
    /// <inheritdoc />
    public partial class AddUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$JAokP465tiyEEzxnmy9lNeOuVLSoExQEOGndyt7x/PtZYcocyy9a6");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$4V4pddiM.kOZ6DKIdac1GObClK/a1bfEDoeLNz3n54rkwm2zrmkzu");
        }
    }
}
