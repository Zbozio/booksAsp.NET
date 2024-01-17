using System;
using System.Collections.Generic;

namespace Bazowane.Models
{
    public partial class Uzytkownik
    {
        public Uzytkownik()
        {
            Komentarzes = new HashSet<Komentarze>();
            Ocenas = new HashSet<Ocena>();
            RecenzjaNaTematAutoras = new HashSet<RecenzjaNaTematAutora>();
            StatusKsiazekUzytkowniks = new HashSet<StatusKsiazekUzytkownik>();
            ZnajomiIdUzytkownikNavigations = new HashSet<Znajomi>();
            ZnajomiUzyIdUzytkownikNavigations = new HashSet<Znajomi>();
            IdListies = new HashSet<Listum>();
        }

        public decimal IdUzytkownik { get; set; }
        public string? Imie { get; set; }
        public string? Nazwisko { get; set; }
        public string? EMail { get; set; }
        public string? Haslo { get; set; }
        public DateTime? DataRejestracji { get; set; }
        public int? IloscOcen { get; set; }
        public int? IloscRecenzji { get; set; }
        public DateTime? OstaniaAktywnosc { get; set; }
        public string? Zdjecie { get; set; }
        public int? Znajomi { get; set; }

        public virtual ICollection<Komentarze> Komentarzes { get; set; }
        public virtual ICollection<Ocena> Ocenas { get; set; }
        public virtual ICollection<RecenzjaNaTematAutora> RecenzjaNaTematAutoras { get; set; }
        public virtual ICollection<StatusKsiazekUzytkownik> StatusKsiazekUzytkowniks { get; set; }
        public virtual ICollection<Znajomi> ZnajomiIdUzytkownikNavigations { get; set; }
        public virtual ICollection<Znajomi> ZnajomiUzyIdUzytkownikNavigations { get; set; }

        public virtual ICollection<Listum> IdListies { get; set; }
    }
}
