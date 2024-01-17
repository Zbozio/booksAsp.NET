using System;
using System.Collections.Generic;

namespace Bazowane.Models
{
    public partial class Ksiazka
    {
        public Ksiazka()
        {
            Komentarzes = new HashSet<Komentarze>();
            Ocenas = new HashSet<Ocena>();
            Polecenia = new HashSet<Polecenium>();
            StatusKsiazekUzytkowniks = new HashSet<StatusKsiazekUzytkownik>();
            IdAutoras = new HashSet<Autor>();
            IdGatunkus = new HashSet<Gatunek>();
            IdListies = new HashSet<Listum>();
        }

        public decimal IdKsiazka { get; set; }
        public decimal? IdStatusu { get; set; }
        public string? Tytul { get; set; }
        public string? Opis { get; set; }
        public DateTime? RokWydania { get; set; }
        public double? SredniaOcena { get; set; }
        public int? LiczbaOcen { get; set; }
        public int? LiczbaRecenzji { get; set; }
        public int? IloscStron { get; set; }

        public virtual Status? IdStatusuNavigation { get; set; }
        public virtual ICollection<Komentarze> Komentarzes { get; set; }
        public virtual ICollection<Ocena> Ocenas { get; set; }
        public virtual ICollection<Polecenium> Polecenia { get; set; }
        public virtual ICollection<StatusKsiazekUzytkownik> StatusKsiazekUzytkowniks { get; set; }

        public virtual ICollection<Autor> IdAutoras { get; set; }
        public virtual ICollection<Gatunek> IdGatunkus { get; set; }
        public virtual ICollection<Listum> IdListies { get; set; }
    }
}
