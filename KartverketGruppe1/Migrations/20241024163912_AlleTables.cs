using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KartverketGruppe1.Migrations
{
    /// <inheritdoc />
    public partial class AlleTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Saksbehandler_ID",
                table: "Saksbehandler",
                newName: "SaksbehandlerID");

            migrationBuilder.RenameColumn(
                name: "Koordinat_ID",
                table: "koordinat",
                newName: "KoordinatID");

            migrationBuilder.RenameColumn(
                name: "Kommune_ID",
                table: "Kommune",
                newName: "KommuneID");

            migrationBuilder.CreateTable(
                name: "Avvikstype",
                columns: table => new
                {
                    AvvikstypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avvikstype", x => x.AvvikstypeID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Bruker",
                columns: table => new
                {
                    BrukerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Fornavn = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Etternavn = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Epost = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Passord = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telefonnummer = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fodselsdato = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Profilbilde = table.Column<byte[]>(type: "longblob", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bruker", x => x.BrukerID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Prioritet",
                columns: table => new
                {
                    PrioritetID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Prioritetsnivå = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prioritet", x => x.PrioritetID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Statustype = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.StatusID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Innmelding",
                columns: table => new
                {
                    InnmeldingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Innmeldingsbeskrivelse = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Dato = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Dokumentasjon = table.Column<byte[]>(type: "longblob", nullable: false),
                    KommuneID = table.Column<int>(type: "int", nullable: false),
                    AvvikstypeID = table.Column<int>(type: "int", nullable: false),
                    BrukerID = table.Column<int>(type: "int", nullable: true),
                    Gjest_epost = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StatusID = table.Column<int>(type: "int", nullable: false),
                    KoordinatID = table.Column<int>(type: "int", nullable: false),
                    PrioritetID = table.Column<int>(type: "int", nullable: true),
                    SaksbehandlerID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Innmelding", x => x.InnmeldingID);
                    table.ForeignKey(
                        name: "FK_Innmelding_Avvikstype_AvvikstypeID",
                        column: x => x.AvvikstypeID,
                        principalTable: "Avvikstype",
                        principalColumn: "AvvikstypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Innmelding_Bruker_BrukerID",
                        column: x => x.BrukerID,
                        principalTable: "Bruker",
                        principalColumn: "BrukerID");
                    table.ForeignKey(
                        name: "FK_Innmelding_Kommune_KommuneID",
                        column: x => x.KommuneID,
                        principalTable: "Kommune",
                        principalColumn: "KommuneID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Innmelding_Prioritet_PrioritetID",
                        column: x => x.PrioritetID,
                        principalTable: "Prioritet",
                        principalColumn: "PrioritetID");
                    table.ForeignKey(
                        name: "FK_Innmelding_Saksbehandler_SaksbehandlerID",
                        column: x => x.SaksbehandlerID,
                        principalTable: "Saksbehandler",
                        principalColumn: "SaksbehandlerID");
                    table.ForeignKey(
                        name: "FK_Innmelding_Status_StatusID",
                        column: x => x.StatusID,
                        principalTable: "Status",
                        principalColumn: "StatusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Innmelding_koordinat_KoordinatID",
                        column: x => x.KoordinatID,
                        principalTable: "koordinat",
                        principalColumn: "KoordinatID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Meldinger",
                columns: table => new
                {
                    MeldingerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Innhold = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SendeTidspunkt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    InnmeldingID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meldinger", x => x.MeldingerID);
                    table.ForeignKey(
                        name: "FK_Meldinger_Innmelding_InnmeldingID",
                        column: x => x.InnmeldingID,
                        principalTable: "Innmelding",
                        principalColumn: "InnmeldingID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Innmelding_AvvikstypeID",
                table: "Innmelding",
                column: "AvvikstypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Innmelding_BrukerID",
                table: "Innmelding",
                column: "BrukerID");

            migrationBuilder.CreateIndex(
                name: "IX_Innmelding_KommuneID",
                table: "Innmelding",
                column: "KommuneID");

            migrationBuilder.CreateIndex(
                name: "IX_Innmelding_KoordinatID",
                table: "Innmelding",
                column: "KoordinatID");

            migrationBuilder.CreateIndex(
                name: "IX_Innmelding_PrioritetID",
                table: "Innmelding",
                column: "PrioritetID");

            migrationBuilder.CreateIndex(
                name: "IX_Innmelding_SaksbehandlerID",
                table: "Innmelding",
                column: "SaksbehandlerID");

            migrationBuilder.CreateIndex(
                name: "IX_Innmelding_StatusID",
                table: "Innmelding",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Meldinger_InnmeldingID",
                table: "Meldinger",
                column: "InnmeldingID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Meldinger");

            migrationBuilder.DropTable(
                name: "Innmelding");

            migrationBuilder.DropTable(
                name: "Avvikstype");

            migrationBuilder.DropTable(
                name: "Bruker");

            migrationBuilder.DropTable(
                name: "Prioritet");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.RenameColumn(
                name: "SaksbehandlerID",
                table: "Saksbehandler",
                newName: "Saksbehandler_ID");

            migrationBuilder.RenameColumn(
                name: "KoordinatID",
                table: "koordinat",
                newName: "Koordinat_ID");

            migrationBuilder.RenameColumn(
                name: "KommuneID",
                table: "Kommune",
                newName: "Kommune_ID");
        }
    }
}
