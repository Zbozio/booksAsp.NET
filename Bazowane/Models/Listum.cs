using System;
using System.Collections.Generic;

namespace Bazowane.Models
{
    public partial class Listum
    {
        public Listum()
        {
            IdKsiazkas = new HashSet<Ksiazka>();
            IdUzytkowniks = new HashSet<Uzytkownik>();
        }

        public decimal IdListy { get; set; }
        public string? NazwaListy { get; set; }
        public string? OpisListy { get; set; }
        public DateTime? DataUtworzenia { get; set; }
        public int? IloscElementow { get; set; }

        public virtual ICollection<Ksiazka> IdKsiazkas { get; set; }
        public virtual ICollection<Uzytkownik> IdUzytkowniks { get; set; }
    }
}
