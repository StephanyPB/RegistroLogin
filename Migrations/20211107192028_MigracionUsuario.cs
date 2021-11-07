using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RegistroLogin.Migrations
{
    public partial class MigracionUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FechaIngreso = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Alias = table.Column<string>(type: "TEXT", nullable: true),
                    Nombres = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Clave = table.Column<string>(type: "TEXT", nullable: true),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.UsuarioId);
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "UsuarioId", "Activo", "Alias", "Clave", "Email", "FechaIngreso", "Nombres" },
                values: new object[] { 1, true, "Joselito", "7110EDA4D09E062AA5E4A390B0A572AC0D2C0220", "josemanuel@gmail.com", new DateTime(2021, 11, 7, 15, 20, 27, 538, DateTimeKind.Local).AddTicks(4814), "Para usuarios" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
