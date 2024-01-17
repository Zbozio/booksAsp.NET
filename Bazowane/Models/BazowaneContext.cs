using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Bazowane.Models
{
    public partial class BazowaneContext : DbContext
    {
        public BazowaneContext()
        {
        }

        public BazowaneContext(DbContextOptions<BazowaneContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Autor> Autors { get; set; } = null!;
        public virtual DbSet<Gatunek> Gatuneks { get; set; } = null!;
        public virtual DbSet<Komentarze> Komentarzes { get; set; } = null!;
        public virtual DbSet<Ksiazka> Ksiazkas { get; set; } = null!;
        public virtual DbSet<Listum> Lista { get; set; } = null!;
        public virtual DbSet<Ocena> Ocenas { get; set; } = null!;
        public virtual DbSet<Polecenium> Polecenia { get; set; } = null!;
        public virtual DbSet<RecenzjaNaTematAutora> RecenzjaNaTematAutoras { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<StatusKsiazekUzytkownik> StatusKsiazekUzytkowniks { get; set; } = null!;
        public virtual DbSet<Uzytkownik> Uzytkowniks { get; set; } = null!;
        public virtual DbSet<Znajomi> Znajomis { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=huj\\SQLEXPRESS;Initial Catalog=Bazowane;Integrated Security=True;TrustServerCertificate=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Autor>(entity =>
            {
                entity.HasKey(e => e.IdAutora);

                entity.ToTable("AUTOR");

                entity.Property(e => e.IdAutora)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID_AUTORA");

                entity.Property(e => e.DataUrodzenia)
                    .HasColumnType("datetime")
                    .HasColumnName("DATA_URODZENIA");

                entity.Property(e => e.ImieAutora)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("IMIE_AUTORA");

                entity.Property(e => e.NazwiskoAutora)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("NAZWISKO_AUTORA");

                entity.Property(e => e.Opis)
                    .HasColumnType("text")
                    .HasColumnName("OPIS");

                entity.Property(e => e.Wiek).HasColumnName("WIEK");

                entity.HasMany(d => d.IdKsiazkas)
                    .WithMany(p => p.IdAutoras)
                    .UsingEntity<Dictionary<string, object>>(
                        "Autorstwo",
                        l => l.HasOne<Ksiazka>().WithMany().HasForeignKey("IdKsiazka").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_AUTORSTW_AUTORSTWO_KSIAZKA"),
                        r => r.HasOne<Autor>().WithMany().HasForeignKey("IdAutora").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_AUTORSTW_AUTORSTWO_AUTOR"),
                        j =>
                        {
                            j.HasKey("IdAutora", "IdKsiazka");

                            j.ToTable("AUTORSTWO");

                            j.HasIndex(new[] { "IdKsiazka" }, "AUTORSTWO2_FK");

                            j.HasIndex(new[] { "IdAutora" }, "AUTORSTWO_FK");

                            j.IndexerProperty<decimal>("IdAutora").HasColumnType("numeric(18, 0)").HasColumnName("ID_AUTORA");

                            j.IndexerProperty<decimal>("IdKsiazka").HasColumnType("numeric(18, 0)").HasColumnName("ID_KSIAZKA");
                        });
            });

            modelBuilder.Entity<Gatunek>(entity =>
            {
                entity.HasKey(e => e.IdGatunku);

                entity.ToTable("GATUNEK");

                entity.Property(e => e.IdGatunku)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID_GATUNKU");

                entity.Property(e => e.NazwaGatunku)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("NAZWA_GATUNKU");
            });

            modelBuilder.Entity<Komentarze>(entity =>
            {
                entity.HasKey(e => e.IdKomentarza);

                entity.ToTable("KOMENTARZE");

                entity.HasIndex(e => e.IdKsiazka, "KOMENTARZE_DO_KSIAZKI_FK");

                entity.HasIndex(e => e.IdUzytkownik, "KOMENTARZE_UZYTKOWNIKA_FK");

                entity.Property(e => e.IdKomentarza)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID_KOMENTARZA");

                entity.Property(e => e.DataKomentarza)
                    .HasColumnType("datetime")
                    .HasColumnName("DATA_KOMENTARZA");

                entity.Property(e => e.IdKsiazka)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("ID_KSIAZKA");

                entity.Property(e => e.IdUzytkownik)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("ID_UZYTKOWNIK");

                entity.Property(e => e.TrescKomentarza)
                    .HasColumnType("text")
                    .HasColumnName("TRESC_KOMENTARZA");

                entity.HasOne(d => d.IdKsiazkaNavigation)
                    .WithMany(p => p.Komentarzes)
                    .HasForeignKey(d => d.IdKsiazka)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KOMENTAR_KOMENTARZ_KSIAZKA");

                entity.HasOne(d => d.IdUzytkownikNavigation)
                    .WithMany(p => p.Komentarzes)
                    .HasForeignKey(d => d.IdUzytkownik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KOMENTAR_KOMENTARZ_UZYTKOWN");
            });

            modelBuilder.Entity<Ksiazka>(entity =>
            {
                entity.HasKey(e => e.IdKsiazka);

                entity.ToTable("KSIAZKA");

                entity.HasIndex(e => e.IdStatusu, "STATUS_KSIAZKI_FK");

                entity.Property(e => e.IdKsiazka)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID_KSIAZKA");

                entity.Property(e => e.IdStatusu)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("ID_STATUSU");

                entity.Property(e => e.IloscStron).HasColumnName("ILOSC_STRON");

                entity.Property(e => e.LiczbaOcen).HasColumnName("LICZBA_OCEN");

                entity.Property(e => e.LiczbaRecenzji).HasColumnName("LICZBA_RECENZJI");

                entity.Property(e => e.Opis)
                    .HasColumnType("text")
                    .HasColumnName("OPIS");

                entity.Property(e => e.RokWydania)
                    .HasColumnType("datetime")
                    .HasColumnName("ROK_WYDANIA");

                entity.Property(e => e.SredniaOcena).HasColumnName("SREDNIA_OCENA");

                entity.Property(e => e.Tytul)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TYTUL");

                entity.HasOne(d => d.IdStatusuNavigation)
                    .WithMany(p => p.Ksiazkas)
                    .HasForeignKey(d => d.IdStatusu)
                    .HasConstraintName("FK_KSIAZKA_STATUS_KS_STATUS");

                entity.HasMany(d => d.IdGatunkus)
                    .WithMany(p => p.IdKsiazkas)
                    .UsingEntity<Dictionary<string, object>>(
                        "Gatunkowosc",
                        l => l.HasOne<Gatunek>().WithMany().HasForeignKey("IdGatunku").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_GATUNKOW_GATUNKOWO_GATUNEK"),
                        r => r.HasOne<Ksiazka>().WithMany().HasForeignKey("IdKsiazka").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_GATUNKOW_GATUNKOWO_KSIAZKA"),
                        j =>
                        {
                            j.HasKey("IdKsiazka", "IdGatunku");

                            j.ToTable("GATUNKOWOSC");

                            j.HasIndex(new[] { "IdGatunku" }, "GATUNKOWOSC2_FK");

                            j.HasIndex(new[] { "IdKsiazka" }, "GATUNKOWOSC_FK");

                            j.IndexerProperty<decimal>("IdKsiazka").HasColumnType("numeric(18, 0)").HasColumnName("ID_KSIAZKA");

                            j.IndexerProperty<decimal>("IdGatunku").HasColumnType("numeric(18, 0)").HasColumnName("ID_GATUNKU");
                        });
            });

            modelBuilder.Entity<Listum>(entity =>
            {
                entity.HasKey(e => e.IdListy);

                entity.ToTable("LISTA");

                entity.Property(e => e.IdListy)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID_LISTY");

                entity.Property(e => e.DataUtworzenia)
                    .HasColumnType("datetime")
                    .HasColumnName("DATA_UTWORZENIA");

                entity.Property(e => e.IloscElementow).HasColumnName("ILOSC_ELEMENTOW");

                entity.Property(e => e.NazwaListy)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("NAZWA_LISTY");

                entity.Property(e => e.OpisListy)
                    .HasColumnType("text")
                    .HasColumnName("OPIS_LISTY");

                entity.HasMany(d => d.IdKsiazkas)
                    .WithMany(p => p.IdListies)
                    .UsingEntity<Dictionary<string, object>>(
                        "DodawanieDoList",
                        l => l.HasOne<Ksiazka>().WithMany().HasForeignKey("IdKsiazka").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_DODAWANI_DODAWANIE_KSIAZKA"),
                        r => r.HasOne<Listum>().WithMany().HasForeignKey("IdListy").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_DODAWANI_DODAWANIE_LISTA"),
                        j =>
                        {
                            j.HasKey("IdListy", "IdKsiazka");

                            j.ToTable("DODAWANIE_DO_LIST");

                            j.HasIndex(new[] { "IdKsiazka" }, "DODAWANIE_DO_LIST2_FK");

                            j.HasIndex(new[] { "IdListy" }, "DODAWANIE_DO_LIST_FK");

                            j.IndexerProperty<decimal>("IdListy").HasColumnType("numeric(18, 0)").HasColumnName("ID_LISTY");

                            j.IndexerProperty<decimal>("IdKsiazka").HasColumnType("numeric(18, 0)").HasColumnName("ID_KSIAZKA");
                        });

                entity.HasMany(d => d.IdUzytkowniks)
                    .WithMany(p => p.IdListies)
                    .UsingEntity<Dictionary<string, object>>(
                        "TworzeniaList",
                        l => l.HasOne<Uzytkownik>().WithMany().HasForeignKey("IdUzytkownik").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_TWORZENI_TWORZENIA_UZYTKOWN"),
                        r => r.HasOne<Listum>().WithMany().HasForeignKey("IdListy").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_TWORZENI_TWORZENIA_LISTA"),
                        j =>
                        {
                            j.HasKey("IdListy", "IdUzytkownik");

                            j.ToTable("TWORZENIA_LIST");

                            j.HasIndex(new[] { "IdUzytkownik" }, "TWORZENIA_LIST2_FK");

                            j.HasIndex(new[] { "IdListy" }, "TWORZENIA_LIST_FK");

                            j.IndexerProperty<decimal>("IdListy").HasColumnType("numeric(18, 0)").HasColumnName("ID_LISTY");

                            j.IndexerProperty<decimal>("IdUzytkownik").HasColumnType("numeric(18, 0)").HasColumnName("ID_UZYTKOWNIK");
                        });
            });

            modelBuilder.Entity<Ocena>(entity =>
            {
                entity.HasKey(e => e.IdOceny);

                entity.ToTable("OCENA");

                entity.HasIndex(e => e.IdKsiazka, "OCENIANIE_KSIAZEK_FK");

                entity.HasIndex(e => e.IdUzytkownik, "OCENY_WYSTAWIONE_FK");

                entity.Property(e => e.IdOceny)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID_OCENY");

                entity.Property(e => e.DataOceny)
                    .HasColumnType("datetime")
                    .HasColumnName("DATA_OCENY");

                entity.Property(e => e.IdKsiazka)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("ID_KSIAZKA");

                entity.Property(e => e.IdUzytkownik)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("ID_UZYTKOWNIK");

                entity.Property(e => e.Ocena1).HasColumnName("OCENA");

                entity.Property(e => e.Opinia)
                    .HasColumnType("text")
                    .HasColumnName("OPINIA");

                entity.HasOne(d => d.IdKsiazkaNavigation)
                    .WithMany(p => p.Ocenas)
                    .HasForeignKey(d => d.IdKsiazka)
                    .HasConstraintName("FK_OCENA_OCENIANIE_KSIAZKA");

                entity.HasOne(d => d.IdUzytkownikNavigation)
                    .WithMany(p => p.Ocenas)
                    .HasForeignKey(d => d.IdUzytkownik)
                    .HasConstraintName("FK_OCENA_OCENY_WYS_UZYTKOWN");
            });

            modelBuilder.Entity<Polecenium>(entity =>
            {
                entity.HasKey(e => e.IdPolecenia);

                entity.ToTable("POLECENIA");

                entity.HasIndex(e => e.IdKsiazka, "POLECENIE_KSIAZKI_FK");

                entity.HasIndex(e => e.IdZnajomosci, "POLECENIE_ZNAJOMEMU_FK");

                entity.Property(e => e.IdPolecenia)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID_POLECENIA");

                entity.Property(e => e.DataPolecenia)
                    .HasColumnType("datetime")
                    .HasColumnName("DATA_POLECENIA");

                entity.Property(e => e.IdKsiazka)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("ID_KSIAZKA");

                entity.Property(e => e.IdZnajomosci)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("ID_ZNAJOMOSCI");

                entity.Property(e => e.TrescPolecenia)
                    .HasColumnType("text")
                    .HasColumnName("TRESC_POLECENIA");

                entity.HasOne(d => d.IdKsiazkaNavigation)
                    .WithMany(p => p.Polecenia)
                    .HasForeignKey(d => d.IdKsiazka)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_POLECENI_POLECENIE_KSIAZKA");

                entity.HasOne(d => d.IdZnajomosciNavigation)
                    .WithMany(p => p.Polecenia)
                    .HasForeignKey(d => d.IdZnajomosci)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_POLECENI_POLECENIE_ZNAJOMI");
            });

            modelBuilder.Entity<RecenzjaNaTematAutora>(entity =>
            {
                entity.HasKey(e => e.IdRecenzji);

                entity.ToTable("RECENZJA_NA_TEMAT_AUTORA");

                entity.HasIndex(e => e.IdUzytkownik, "RECENZJA_UZYTKOWNIK_FK");

                entity.HasIndex(e => e.IdAutora, "RECENZOWANIE_AUTORA_FK");

                entity.Property(e => e.IdRecenzji)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID_RECENZJI");

                entity.Property(e => e.DataRecenzji)
                    .HasColumnType("datetime")
                    .HasColumnName("DATA_RECENZJI");

                entity.Property(e => e.IdAutora)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("ID_AUTORA");

                entity.Property(e => e.IdUzytkownik)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("ID_UZYTKOWNIK");

                entity.Property(e => e.OcenaAutora).HasColumnName("OCENA_AUTORA");

                entity.Property(e => e.RecenzjaTeskt)
                    .HasColumnType("text")
                    .HasColumnName("RECENZJA_TESKT");

                entity.HasOne(d => d.IdAutoraNavigation)
                    .WithMany(p => p.RecenzjaNaTematAutoras)
                    .HasForeignKey(d => d.IdAutora)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RECENZJA_RECENZOWA_AUTOR");

                entity.HasOne(d => d.IdUzytkownikNavigation)
                    .WithMany(p => p.RecenzjaNaTematAutoras)
                    .HasForeignKey(d => d.IdUzytkownik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RECENZJA_RECENZJA__UZYTKOWN");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.IdStatusu);

                entity.ToTable("STATUS");

                entity.Property(e => e.IdStatusu)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID_STATUSU");

                entity.Property(e => e.StanStatusu)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("STAN_STATUSU");
            });

            modelBuilder.Entity<StatusKsiazekUzytkownik>(entity =>
            {
                entity.HasKey(e => new { e.IdStatusu, e.IdUzytkownik, e.IdKsiazka });

                entity.ToTable("STATUS_KSIAZEK_UZYTKOWNIK");

                entity.HasIndex(e => e.IdUzytkownik, "STATUS_KSIAZEK_UZYTKOWNIK2_FK");

                entity.HasIndex(e => e.IdStatusu, "STATUS_KSIAZEK_UZYTKOWNIK_FK");

                entity.Property(e => e.IdStatusu)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("ID_STATUSU");

                entity.Property(e => e.IdUzytkownik)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("ID_UZYTKOWNIK");

                entity.Property(e => e.IdKsiazka)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("ID_KSIAZKA");

                entity.HasOne(d => d.IdKsiazkaNavigation)
                    .WithMany(p => p.StatusKsiazekUzytkowniks)
                    .HasForeignKey(d => d.IdKsiazka)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_STATUS_K_REFERENCE_KSIAZKA");

                entity.HasOne(d => d.IdStatusuNavigation)
                    .WithMany(p => p.StatusKsiazekUzytkowniks)
                    .HasForeignKey(d => d.IdStatusu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_STATUS_K_STATUS_KS_STATUS");

                entity.HasOne(d => d.IdUzytkownikNavigation)
                    .WithMany(p => p.StatusKsiazekUzytkowniks)
                    .HasForeignKey(d => d.IdUzytkownik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_STATUS_K_STATUS_KS_UZYTKOWN");
            });

            modelBuilder.Entity<Uzytkownik>(entity =>
            {
                entity.HasKey(e => e.IdUzytkownik);

                entity.ToTable("UZYTKOWNIK");

                entity.Property(e => e.IdUzytkownik)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID_UZYTKOWNIK");

                entity.Property(e => e.DataRejestracji)
                    .HasColumnType("datetime")
                    .HasColumnName("DATA_REJESTRACJI");

                entity.Property(e => e.EMail)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("E_MAIL");

                entity.Property(e => e.Haslo)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("HASLO");

                entity.Property(e => e.IloscOcen).HasColumnName("ILOSC_OCEN");

                entity.Property(e => e.IloscRecenzji).HasColumnName("ILOSC_RECENZJI");

                entity.Property(e => e.Imie)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("IMIE");

                entity.Property(e => e.Nazwisko)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAZWISKO");

                entity.Property(e => e.OstaniaAktywnosc)
                    .HasColumnType("datetime")
                    .HasColumnName("OSTANIA_AKTYWNOSC");

                entity.Property(e => e.Zdjecie)
                    .HasColumnType("text")
                    .HasColumnName("ZDJECIE");

                entity.Property(e => e.Znajomi).HasColumnName("ZNAJOMI");
            });

            modelBuilder.Entity<Znajomi>(entity =>
            {
                entity.HasKey(e => e.IdZnajomosci);

                entity.ToTable("ZNAJOMI");

                entity.HasIndex(e => e.UzyIdUzytkownik, "RELACJA_FK");

                entity.HasIndex(e => e.IdUzytkownik, "ZNAJOMOSC_FK");

                entity.Property(e => e.IdZnajomosci)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID_ZNAJOMOSCI");

                entity.Property(e => e.DataZnajomosci)
                    .HasColumnType("datetime")
                    .HasColumnName("DATA_ZNAJOMOSCI");

                entity.Property(e => e.IdUzytkownik)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("ID_UZYTKOWNIK");

                entity.Property(e => e.IdZapraszajacego).HasColumnName("ID_ZAPRASZAJACEGO");

                entity.Property(e => e.IdZapraszanego).HasColumnName("ID_ZAPRASZANEGO");

                entity.Property(e => e.NotatkiOZnajomym)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("NOTATKI_O_ZNAJOMYM");

                entity.Property(e => e.StatusZnajomosci)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("STATUS_ZNAJOMOSCI");

                entity.Property(e => e.UzyIdUzytkownik)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("UZY_ID_UZYTKOWNIK");

                entity.HasOne(d => d.IdUzytkownikNavigation)
                    .WithMany(p => p.ZnajomiIdUzytkownikNavigations)
                    .HasForeignKey(d => d.IdUzytkownik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ZNAJOMI_ZNAJOMOSC_UZYTKOWN");

                entity.HasOne(d => d.UzyIdUzytkownikNavigation)
                    .WithMany(p => p.ZnajomiUzyIdUzytkownikNavigations)
                    .HasForeignKey(d => d.UzyIdUzytkownik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ZNAJOMI_RELACJA_UZYTKOWN");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
