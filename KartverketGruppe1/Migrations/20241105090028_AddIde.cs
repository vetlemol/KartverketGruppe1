using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KartverketGruppe1.Migrations
{
    /// <inheritdoc />
    public partial class AddIde : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Innmelding_Bruker_BrukerID",
                table: "Innmelding");

            migrationBuilder.DropForeignKey(
                name: "FK_Innmelding_Saksbehandler_SaksbehandlerID",
                table: "Innmelding");

            migrationBuilder.DropTable(
                name: "Bruker");

            migrationBuilder.DropTable(
                name: "Saksbehandler");

            migrationBuilder.DropColumn(
                name: "Epost",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Passord",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Telefonnummer",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "SaksbehandlerID",
                table: "Innmelding",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "BrukerID",
                table: "Innmelding",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Innmelding_AspNetUsers_BrukerID",
                table: "Innmelding",
                column: "BrukerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Innmelding_AspNetUsers_SaksbehandlerID",
                table: "Innmelding",
                column: "SaksbehandlerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Innmelding_AspNetUsers_BrukerID",
                table: "Innmelding");

            migrationBuilder.DropForeignKey(
                name: "FK_Innmelding_AspNetUsers_SaksbehandlerID",
                table: "Innmelding");

            migrationBuilder.AlterColumn<int>(
                name: "SaksbehandlerID",
                table: "Innmelding",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "BrukerID",
                table: "Innmelding",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Epost",
                table: "AspNetUsers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Passord",
                table: "AspNetUsers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Telefonnummer",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Bruker",
                columns: table => new
                {
                    BrukerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Epost = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Etternavn = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fodselsdato = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Fornavn = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Passord = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Profilbilde = table.Column<byte[]>(type: "longblob", nullable: true),
                    Telefonnummer = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bruker", x => x.BrukerID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Saksbehandler",
                columns: table => new
                {
                    SaksbehandlerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ansvarsområde = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Avdeling = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Epost = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Etternavn = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fornavn = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Passord = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Profilbilde = table.Column<byte[]>(type: "longblob", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Saksbehandler", x => x.SaksbehandlerID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Bruker_Epost",
                table: "Bruker",
                column: "Epost",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Saksbehandler_Epost",
                table: "Saksbehandler",
                column: "Epost",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Innmelding_Bruker_BrukerID",
                table: "Innmelding",
                column: "BrukerID",
                principalTable: "Bruker",
                principalColumn: "BrukerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Innmelding_Saksbehandler_SaksbehandlerID",
                table: "Innmelding",
                column: "SaksbehandlerID",
                principalTable: "Saksbehandler",
                principalColumn: "SaksbehandlerID");
        }
    }
}
