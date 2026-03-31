using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCursosTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Cursos");

            migrationBuilder.AddColumn<int>(
                name: "año",
                table: "Cursos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "division",
                table: "Cursos",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "año",
                table: "Cursos");

            migrationBuilder.DropColumn(
                name: "division",
                table: "Cursos");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Cursos",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
