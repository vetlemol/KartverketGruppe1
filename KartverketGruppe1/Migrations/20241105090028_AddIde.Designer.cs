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
    [Migration("20241105090028_AddIde")]
    partial class AddIde
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

            modelBuilder.Entity("KartverketGruppe1.Data.Innmelding", b =>
                {
                    b.Property<int>("InnmeldingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("InnmeldingID"));

                    b.Property<int>("AvvikstypeID")
                        .HasColumnType("int");

                    b.Property<string>("BrukerID")
                        .HasColumnType("varchar(255)");

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

                    b.Property<string>("SaksbehandlerID")
                        .HasColumnType("varchar(255)");

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

            modelBuilder.Entity("KartverketGruppe1.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Etternavn")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("Fodselsdato")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Fornavn")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<byte[]>("Profilbilde")
                        .HasColumnType("longblob");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("KartverketGruppe1.Data.Innmelding", b =>
                {
                    b.HasOne("KartverketGruppe1.Data.Avvikstype", "Avvikstype")
                        .WithMany()
                        .HasForeignKey("AvvikstypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KartverketGruppe1.Models.ApplicationUser", "Bruker")
                        .WithMany("Innmeldinger")
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

                    b.HasOne("KartverketGruppe1.Models.ApplicationUser", "Saksbehandler")
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("KartverketGruppe1.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("KartverketGruppe1.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KartverketGruppe1.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("KartverketGruppe1.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("KartverketGruppe1.Models.ApplicationUser", b =>
                {
                    b.Navigation("Innmeldinger");
                });
#pragma warning restore 612, 618
        }
    }
}
