﻿// <auto-generated />
using System;
using Kino.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Kino.Migrations
{
    [DbContext(typeof(KinoContext))]
    partial class KinoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf8mb4");
            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Kino.Models.Bilet", b =>
                {
                    b.Property<int>("IdBilet")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id_bilet");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdBilet"));

                    b.Property<string>("KlientLogin")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("SeansIdSeans")
                        .HasColumnType("int")
                        .HasColumnName("SeansId_seans");

                    b.Property<int>("ZarezerwowaneMiejsca")
                        .HasColumnType("int")
                        .HasColumnName("zarezerwowane_miejsca");

                    b.HasKey("IdBilet")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "KlientLogin" }, "KlientLogin");

                    b.ToTable("bilet", (string)null);
                });

            modelBuilder.Entity("Kino.Models.Film", b =>
                {
                    b.Property<int>("IdFilm")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id_film");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdFilm"));

                    b.Property<int>("CzasTrwania")
                        .HasColumnType("int")
                        .HasColumnName("czas_trwania");

                    b.Property<string>("ImieNazwiskoRezysera")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("Imie_nazwisko_rezysera");

                    b.Property<string>("KrajProdukcji")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("Kraj_produkcji");

                    b.Property<string>("Opis")
                        .HasColumnType("text")
                        .HasColumnName("opis");

                    b.Property<string>("RokWydania")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("Rok_wydania");

                    b.Property<string>("Tytul")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("IdFilm")
                        .HasName("PRIMARY");

                    b.ToTable("film", (string)null);
                });

            modelBuilder.Entity("Kino.Models.Karnet", b =>
                {
                    b.Property<int>("IdKarnet")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id_karnet");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdKarnet"));

                    b.Property<string>("KlientLogin")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("ZarezerwowaneMiejsca")
                        .HasColumnType("int")
                        .HasColumnName("zarezerwowane_miejsca");

                    b.HasKey("IdKarnet")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "KlientLogin" }, "KlientLogin")
                        .HasDatabaseName("KlientLogin1");

                    b.ToTable("karnet", (string)null);
                });

            modelBuilder.Entity("Kino.Models.KarnetSean", b =>
                {
                    b.Property<int>("KarnetIdKarnet")
                        .HasColumnType("int")
                        .HasColumnName("KarnetId_karnet");

                    b.Property<int>("SeansIdSeans")
                        .HasColumnType("int")
                        .HasColumnName("SeansId_seans");

                    b.HasKey("KarnetIdKarnet", "SeansIdSeans")
                        .HasName("PRIMARY")
                        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                    b.ToTable("karnet_seans", (string)null);
                });

            modelBuilder.Entity("Kino.Models.Klient", b =>
                {
                    b.Property<string>("Login")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Haslo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Imie")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Nazwisko")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Login")
                        .HasName("PRIMARY");

                    b.ToTable("klient", (string)null);
                });

            modelBuilder.Entity("Kino.Models.Pracownik", b =>
                {
                    b.Property<int>("PracownikId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("int")
                        .HasColumnName("Pracownik_Id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("PracownikId"));

                    b.Property<string>("KlientLogin")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("PracownikId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "KlientLogin" }, "KlientLogin")
                        .HasDatabaseName("KlientLogin2");

                    b.ToTable("pracownik", (string)null);
                });

            modelBuilder.Entity("Kino.Models.Sala", b =>
                {
                    b.Property<int>("NumerSali")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Numer_sali");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("NumerSali"));

                    b.Property<int>("LiczbaMiejsc")
                        .HasColumnType("int")
                        .HasColumnName("Liczba_miejsc");

                    b.HasKey("NumerSali")
                        .HasName("PRIMARY");

                    b.ToTable("sala", (string)null);
                });

            modelBuilder.Entity("Kino.Models.Sean", b =>
                {
                    b.Property<int>("IdSeans")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id_seans");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdSeans"));

                    b.Property<int>("FilmIdFilm")
                        .HasColumnType("int")
                        .HasColumnName("FilmId_film");

                    b.Property<int>("SalaNumerSali")
                        .HasColumnType("int")
                        .HasColumnName("SalaNumer_sali");

                    b.Property<DateTime>("TerminRozpoczecia")
                        .HasColumnType("datetime")
                        .HasColumnName("Termin_rozpoczecia");

                    b.Property<DateTime>("TerminZakonczenia")
                        .HasColumnType("datetime")
                        .HasColumnName("Termin_zakonczenia");

                    b.Property<int>("WolneMiejsca")
                        .HasColumnType("int")
                        .HasColumnName("wolne_miejsca");

                    b.HasKey("IdSeans")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "FilmIdFilm" }, "FilmId_film");

                    b.ToTable("seans", (string)null);
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("varchar(21)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

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

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");

                    b.UseTphMappingStrategy();
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

            modelBuilder.Entity("Kino.Areas.Identity.Pages.Account.ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("Imie")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nazwisko")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasDiscriminator().HasValue("ApplicationUser");
                });

            modelBuilder.Entity("Kino.Models.Bilet", b =>
                {
                    b.HasOne("Kino.Models.Klient", "KlientLoginNavigation")
                        .WithMany("Bilets")
                        .HasForeignKey("KlientLogin")
                        .IsRequired()
                        .HasConstraintName("bilet_ibfk_1");

                    b.Navigation("KlientLoginNavigation");
                });

            modelBuilder.Entity("Kino.Models.Karnet", b =>
                {
                    b.HasOne("Kino.Models.Klient", "KlientLoginNavigation")
                        .WithMany("Karnets")
                        .HasForeignKey("KlientLogin")
                        .IsRequired()
                        .HasConstraintName("karnet_ibfk_1");

                    b.Navigation("KlientLoginNavigation");
                });

            modelBuilder.Entity("Kino.Models.Pracownik", b =>
                {
                    b.HasOne("Kino.Models.Klient", "KlientLoginNavigation")
                        .WithMany("Pracowniks")
                        .HasForeignKey("KlientLogin")
                        .IsRequired()
                        .HasConstraintName("pracownik_ibfk_1");

                    b.Navigation("KlientLoginNavigation");
                });

            modelBuilder.Entity("Kino.Models.Sean", b =>
                {
                    b.HasOne("Kino.Models.Film", "FilmIdFilmNavigation")
                        .WithMany("Seans")
                        .HasForeignKey("FilmIdFilm")
                        .IsRequired()
                        .HasConstraintName("seans_ibfk_1");

                    b.Navigation("FilmIdFilmNavigation");
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
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
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

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Kino.Models.Film", b =>
                {
                    b.Navigation("Seans");
                });

            modelBuilder.Entity("Kino.Models.Klient", b =>
                {
                    b.Navigation("Bilets");

                    b.Navigation("Karnets");

                    b.Navigation("Pracowniks");
                });
#pragma warning restore 612, 618
        }
    }
}
