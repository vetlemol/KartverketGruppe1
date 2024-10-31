﻿// <auto-generated />
using System;
using KartverketGruppe1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KartverketGruppe1.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241031101448_InitialCreateHedda")]
    partial class InitialCreateHedda
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("KartverketGruppe1.Data.Avvikstype", b =>
                {
                    b.Property<int>("AvvikstypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("AvvikstypeID"));

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("AvvikstypeID");

                    b.ToTable("Avvikstype");
                });

            modelBuilder.Entity("KartverketGruppe1.Data.Bruker", b =>
                {
                    b.Property<int>("BrukerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("BrukerID"));

                    b.Property<string>("Epost")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Etternavn")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("Fodselsdato")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Fornavn")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Passord")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<byte[]>("Profilbilde")
                        .HasColumnType("longblob");

                    b.Property<string>("Telefonnummer")
                        .HasColumnType("longtext");

                    b.HasKey("BrukerID");

                    b.HasIndex("Epost")
                        .IsUnique();

                    b.ToTable("Bruker");
                });

            modelBuilder.Entity("KartverketGruppe1.Data.Innmelding", b =>
                {
                    b.Property<int>("InnmeldingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("InnmeldingID"));

                    b.Property<int>("AvvikstypeID")
                        .HasColumnType("int");

                    b.Property<int?>("BrukerID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Dato")
                        .HasColumnType("datetime(6)");

                    b.Property<byte[]>("Dokumentasjon")
                        .HasColumnType("longblob");

                    b.Property<string>("Gjest_epost")
                        .HasColumnType("longtext");

                    b.Property<string>("Innmeldingsbeskrivelse")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("KommuneID")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("KoordinatID")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("PrioritetID")
                        .HasColumnType("int");

                    b.Property<int?>("SaksbehandlerID")
                        .HasColumnType("int");

                    b.Property<int>("StatusID")
                        .HasColumnType("int");

                    b.HasKey("InnmeldingID");

                    b.HasIndex("AvvikstypeID");

                    b.HasIndex("BrukerID");

                    b.HasIndex("KommuneID");

                    b.HasIndex("KoordinatID");

                    b.HasIndex("PrioritetID");

                    b.HasIndex("SaksbehandlerID");

                    b.HasIndex("StatusID");

                    b.ToTable("Innmelding");
                });

            modelBuilder.Entity("KartverketGruppe1.Data.Kommune", b =>
                {
                    b.Property<int>("KommuneID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("KommuneID"));

                    b.Property<string>("Kommunenavn")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Kommunenummer")
                        .HasColumnType("int");

                    b.HasKey("KommuneID");

                    b.ToTable("Kommune");
                });

            modelBuilder.Entity("KartverketGruppe1.Data.Koordinat", b =>
                {
                    b.Property<int>("KoordinatID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("KoordinatID"));

                    b.Property<string>("Koordinater")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("KoordinatID");

                    b.ToTable("Koordinat");
                });

            modelBuilder.Entity("KartverketGruppe1.Data.Meldinger", b =>
                {
                    b.Property<int>("MeldingerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("MeldingerID"));

                    b.Property<string>("Innhold")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("InnmeldingID")
                        .HasColumnType("int");

                    b.Property<DateTime>("SendeTidspunkt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("MeldingerID");

                    b.HasIndex("InnmeldingID");

                    b.ToTable("Meldinger");
                });

            modelBuilder.Entity("KartverketGruppe1.Data.Prioritet", b =>
                {
                    b.Property<int>("PrioritetID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("PrioritetID"));

                    b.Property<string>("Prioritetsnivå")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("PrioritetID");

                    b.ToTable("Prioritet");
                });

            modelBuilder.Entity("KartverketGruppe1.Data.Saksbehandler", b =>
                {
                    b.Property<int>("SaksbehandlerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("SaksbehandlerID"));

                    b.Property<string>("Ansvarsområde")
                        .HasColumnType("longtext");

                    b.Property<string>("Avdeling")
                        .HasColumnType("longtext");

                    b.Property<string>("Epost")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Etternavn")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Fornavn")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Passord")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<byte[]>("Profilbilde")
                        .HasColumnType("longblob");

                    b.HasKey("SaksbehandlerID");

                    b.HasIndex("Epost")
                        .IsUnique();

                    b.ToTable("Saksbehandler");
                });

            modelBuilder.Entity("KartverketGruppe1.Data.Status", b =>
                {
                    b.Property<int>("StatusID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("StatusID"));

                    b.Property<string>("Statustype")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("StatusID");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("KartverketGruppe1.Data.Innmelding", b =>
                {
                    b.HasOne("KartverketGruppe1.Data.Avvikstype", "Avvikstype")
                        .WithMany()
                        .HasForeignKey("AvvikstypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KartverketGruppe1.Data.Bruker", "Bruker")
                        .WithMany()
                        .HasForeignKey("BrukerID");

                    b.HasOne("KartverketGruppe1.Data.Kommune", "Kommune")
                        .WithMany()
                        .HasForeignKey("KommuneID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KartverketGruppe1.Data.Koordinat", "Koordinat")
                        .WithMany()
                        .HasForeignKey("KoordinatID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KartverketGruppe1.Data.Prioritet", "Prioritet")
                        .WithMany()
                        .HasForeignKey("PrioritetID");

                    b.HasOne("KartverketGruppe1.Data.Saksbehandler", "Saksbehandler")
                        .WithMany()
                        .HasForeignKey("SaksbehandlerID");

                    b.HasOne("KartverketGruppe1.Data.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Avvikstype");

                    b.Navigation("Bruker");

                    b.Navigation("Kommune");

                    b.Navigation("Koordinat");

                    b.Navigation("Prioritet");

                    b.Navigation("Saksbehandler");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("KartverketGruppe1.Data.Meldinger", b =>
                {
                    b.HasOne("KartverketGruppe1.Data.Innmelding", "Innmelding")
                        .WithMany()
                        .HasForeignKey("InnmeldingID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Innmelding");
                });
#pragma warning restore 612, 618
        }
    }
}
