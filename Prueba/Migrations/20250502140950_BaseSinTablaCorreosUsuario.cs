using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tienda_Virtual.Migrations
{
    /// <inheritdoc />
    public partial class BaseSinTablaCorreosUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CorreosUsuario");

            migrationBuilder.AddColumn<string>(
                name: "CorreoUsuario",
                table: "Usuario",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "UC_CorreoUser",
                table: "Usuario",
                column: "CorreoUsuario",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UC_CorreoUser",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "CorreoUsuario",
                table: "Usuario");

            migrationBuilder.CreateTable(
                name: "CorreosUsuario",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    CorreoUsuario = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_IdUsuarioCorreo",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CorreosUsuario_IdUsuario",
                table: "CorreosUsuario",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "UC_Correo",
                table: "CorreosUsuario",
                column: "CorreoUsuario",
                unique: true,
                filter: "[CorreoUsuario] IS NOT NULL");
        }
    }
}
