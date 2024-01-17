using System;
using System.Collections.Generic;

namespace Bazowane.Models
{
    public partial class Komentarze
    {
        public decimal IdKomentarza { get; set; }
        public decimal IdUzytkownik { get; set; }
        public decimal IdKsiazka { get; set; }
        public string? TrescKomentarza { get; set; }
        public DateTime? DataKomentarza { get; set; }

        public virtual Ksiazka IdKsiazkaNavigation { get; set; } = null!;
        public virtual Uzytkownik IdUzytkownikNavigation { get; set; } = null!;
    }
}
