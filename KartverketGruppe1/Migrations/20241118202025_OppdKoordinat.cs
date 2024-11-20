using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KartverketGruppe1.Migrations
{
    /// <inheritdoc />
    public partial class OppdKoordinat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GeometryType",
                table: "Koordinat",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GeometryType",
                table: "Koordinat");
        }
    }
}
