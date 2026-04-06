using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAlumnsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "año",
                table: "Alumnos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "division",
                table: "Alumnos",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "año",
                table: "Alumnos");

            migrationBuilder.DropColumn(
                name: "division",
                table: "Alumnos");
        }
    }
}
