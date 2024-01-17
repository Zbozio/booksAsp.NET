using System;
using System.Collections.Generic;

namespace Bazowane.Models
{
    public partial class Znajomi
    {
        public Znajomi()
        {
            Polecenia = new HashSet<Polecenium>();
        }

        public decimal IdZnajomosci { get; set; }
        public decimal IdUzytkownik { get; set; }
        public decimal UzyIdUzytkownik { get; set; }
        public long? IdZapraszajacego { get; set; }
        public long? IdZapraszanego { get; set; }
        public string? StatusZnajomosci { get; set; }
        public DateTime? DataZnajomosci { get; set; }
        public string? NotatkiOZnajomym { get; set; }

        public virtual Uzytkownik IdUzytkownikNavigation { get; set; } = null!;
        public virtual Uzytkownik UzyIdUzytkownikNavigation { get; set; } = null!;
        public virtual ICollection<Polecenium> Polecenia { get; set; }
    }
}
