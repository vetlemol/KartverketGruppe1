using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KartverketGruppe1.Migrations
{
    /// <inheritdoc />
    public partial class OppdaterKommuneOgInnmelding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Innmelding_Koordinat_KoordinatID",
                table: "Innmelding");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Koordinat",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Koordinat",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<int>(
                name: "KoordinatID",
                table: "Innmelding",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Innmelding_Koordinat_KoordinatID",
                table: "Innmelding",
                column: "KoordinatID",
                principalTable: "Koordinat",
                principalColumn: "KoordinatID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Innmelding_Koordinat_KoordinatID",
                table: "Innmelding");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Koordinat");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Koordinat");

            migrationBuilder.AlterColumn<int>(
                name: "KoordinatID",
                table: "Innmelding",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Innmelding_Koordinat_KoordinatID",
                table: "Innmelding",
                column: "KoordinatID",
                principalTable: "Koordinat",
                principalColumn: "KoordinatID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
