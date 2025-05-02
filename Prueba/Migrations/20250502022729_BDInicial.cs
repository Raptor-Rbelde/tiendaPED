using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tienda_Virtual.Migrations
{
    /// <inheritdoc />
    public partial class BDInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    Contrasenia = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdUsuario", x => x.IdUsuario);
                });

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

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    NombreProducto = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Descripcion = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdProducto", x => x.IdProducto);
                    table.UniqueConstraint("AK_Producto_IdProducto_NombreProducto", x => new { x.IdProducto, x.NombreProducto });
                    table.ForeignKey(
                        name: "FK_IdUsuarioProducto",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Factura",
                columns: table => new
                {
                    IdFactura = table.Column<int>(type: "int", nullable: false),
                    FechaCompra = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IdProducto = table.Column<int>(type: "int", nullable: true),
                    NombreProducto = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdFactura", x => x.IdFactura);
                    table.ForeignKey(
                        name: "FK_DatosFactura",
                        columns: x => new { x.IdProducto, x.NombreProducto },
                        principalTable: "Producto",
                        principalColumns: new[] { "IdProducto", "NombreProducto" },
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

            migrationBuilder.CreateIndex(
                name: "IX_Factura_IdProducto_NombreProducto",
                table: "Factura",
                columns: new[] { "IdProducto", "NombreProducto" });

            migrationBuilder.CreateIndex(
                name: "IX_Producto_IdUsuario",
                table: "Producto",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "UQ_IdProducto_NombreProducto",
                table: "Producto",
                columns: new[] { "IdProducto", "NombreProducto" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CorreosUsuario");

            migrationBuilder.DropTable(
                name: "Factura");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
