using System;
using System.Collections.Generic;
using Kino.Areas.Identity.Pages.Account;
using Kino.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Kino.Data;

public partial class KinoContext : IdentityDbContext<IdentityUser>
{
    public KinoContext()
    {
    }

    public KinoContext(DbContextOptions<KinoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public virtual DbSet<Bilet> Bilets { get; set; }

    public virtual DbSet<Film> Films { get; set; }

    public virtual DbSet<Karnet> Karnets { get; set; }

    public virtual DbSet<KarnetSean> KarnetSeans { get; set; }

    public virtual DbSet<Klient> Klients { get; set; }

    public virtual DbSet<Pracownik> Pracowniks { get; set; }

    public virtual DbSet<Sala> Salas { get; set; }

    public virtual DbSet<Sean> Seans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=kino;user=root;password=!Rezerwacja1", ServerVersion.Parse("8.0.37-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Bilet>(entity =>
        {
            entity.HasKey(e => e.IdBilet).HasName("PRIMARY");

            entity.ToTable("bilet");

            entity.HasIndex(e => e.KlientLogin, "KlientLogin");

            entity.Property(e => e.IdBilet).HasColumnName("Id_bilet");
            entity.Property(e => e.KlientLogin).HasMaxLength(50);
            entity.Property(e => e.SeansIdSeans).HasColumnName("SeansId_seans");
            entity.Property(e => e.ZarezerwowaneMiejsca).HasColumnName("zarezerwowane_miejsca");

            entity.HasOne(d => d.KlientLoginNavigation).WithMany(p => p.Bilets)
                .HasForeignKey(d => d.KlientLogin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("bilet_ibfk_1");
        });

        modelBuilder.Entity<Film>(entity =>
        {
            entity.HasKey(e => e.IdFilm).HasName("PRIMARY");

            entity.ToTable("film");

            entity.Property(e => e.IdFilm).HasColumnName("Id_film");
            entity.Property(e => e.CzasTrwania).HasColumnName("czas_trwania");
            entity.Property(e => e.ImieNazwiskoRezysera)
                .HasMaxLength(255)
                .HasColumnName("Imie_nazwisko_rezysera");
            entity.Property(e => e.KrajProdukcji)
                .HasMaxLength(30)
                .HasColumnName("Kraj_produkcji");
            entity.Property(e => e.Opis)
                .HasColumnType("text")
                .HasColumnName("opis");
            entity.Property(e => e.RokWydania)
                .HasMaxLength(255)
                .HasColumnName("Rok_wydania");
            entity.Property(e => e.Tytul).HasMaxLength(255);
        });

        modelBuilder.Entity<Karnet>(entity =>
        {
            entity.HasKey(e => e.IdKarnet).HasName("PRIMARY");

            entity.ToTable("karnet");

            entity.HasIndex(e => e.KlientLogin, "KlientLogin");

            entity.Property(e => e.IdKarnet).HasColumnName("Id_karnet");
            entity.Property(e => e.KlientLogin).HasMaxLength(50);
            entity.Property(e => e.ZarezerwowaneMiejsca).HasColumnName("zarezerwowane_miejsca");

            entity.HasOne(d => d.KlientLoginNavigation).WithMany(p => p.Karnets)
                .HasForeignKey(d => d.KlientLogin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("karnet_ibfk_1");
        });

        modelBuilder.Entity<KarnetSean>(entity =>
        {
            entity.HasKey(e => new { e.KarnetIdKarnet, e.SeansIdSeans })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("karnet_seans");

            entity.Property(e => e.KarnetIdKarnet).HasColumnName("KarnetId_karnet");
            entity.Property(e => e.SeansIdSeans).HasColumnName("SeansId_seans");
        });

        modelBuilder.Entity<Klient>(entity =>
        {
            entity.HasKey(e => e.Login).HasName("PRIMARY");

            entity.ToTable("klient");

            entity.Property(e => e.Login).HasMaxLength(50);
            entity.Property(e => e.Haslo).HasMaxLength(50);
            entity.Property(e => e.Imie).HasMaxLength(50);
            entity.Property(e => e.Nazwisko).HasMaxLength(50);
        });

        modelBuilder.Entity<Pracownik>(entity =>
        {
            entity.HasKey(e => e.PracownikId).HasName("PRIMARY");

            entity.ToTable("pracownik");

            entity.HasIndex(e => e.KlientLogin, "KlientLogin");

            entity.Property(e => e.PracownikId)
                .HasMaxLength(50)
                .HasColumnName("Pracownik_Id");
            entity.Property(e => e.KlientLogin).HasMaxLength(50);

            entity.HasOne(d => d.KlientLoginNavigation).WithMany(p => p.Pracowniks)
                .HasForeignKey(d => d.KlientLogin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pracownik_ibfk_1");
        });

        modelBuilder.Entity<Sala>(entity =>
        {
            entity.HasKey(e => e.NumerSali).HasName("PRIMARY");

            entity.ToTable("sala");

            entity.Property(e => e.NumerSali).HasColumnName("Numer_sali");
            entity.Property(e => e.LiczbaMiejsc).HasColumnName("Liczba_miejsc");
        });

        modelBuilder.Entity<Sean>(entity =>
        {
            entity.HasKey(e => e.IdSeans).HasName("PRIMARY");

            entity.ToTable("seans");

            entity.HasIndex(e => e.FilmIdFilm, "FilmId_film");

            entity.Property(e => e.IdSeans).HasColumnName("Id_seans");
            entity.Property(e => e.FilmIdFilm).HasColumnName("FilmId_film");
            entity.Property(e => e.SalaNumerSali).HasColumnName("SalaNumer_sali");
            entity.Property(e => e.TerminRozpoczecia)
                .HasColumnType("datetime")
                .HasColumnName("Termin_rozpoczecia");
            entity.Property(e => e.TerminZakonczenia)
                .HasColumnType("datetime")
                .HasColumnName("Termin_zakonczenia");
            entity.Property(e => e.WolneMiejsca).HasColumnName("wolne_miejsca");

            entity.HasOne(d => d.FilmIdFilmNavigation).WithMany(p => p.Seans)
                .HasForeignKey(d => d.FilmIdFilm)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("seans_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    //internal object FirstOrDefault(Func<object, object> value)
    //{
    //    throw new NotImplementedException();
    //}

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
