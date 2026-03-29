using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionApi.Migrations
{
    /// <inheritdoc />
    public partial class AddCursosTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alumnos_Curso_CursoID",
                table: "Alumnos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Curso",
                table: "Curso");

            migrationBuilder.RenameTable(
                name: "Curso",
                newName: "Cursos");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Fecha",
                table: "Asistencias",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cursos",
                table: "Cursos",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Alumnos_Cursos_CursoID",
                table: "Alumnos",
                column: "CursoID",
                principalTable: "Cursos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alumnos_Cursos_CursoID",
                table: "Alumnos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cursos",
                table: "Cursos");

            migrationBuilder.RenameTable(
                name: "Cursos",
                newName: "Curso");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Fecha",
                table: "Asistencias",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Curso",
                table: "Curso",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Alumnos_Curso_CursoID",
                table: "Alumnos",
                column: "CursoID",
                principalTable: "Curso",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
