using System;
using System.Collections.Generic;

namespace Bazowane.Models
{
    public partial class Autor
    {
        public Autor()
        {
            RecenzjaNaTematAutoras = new HashSet<RecenzjaNaTematAutora>();
            IdKsiazkas = new HashSet<Ksiazka>();
        }

        public decimal IdAutora { get; set; }
        public string? ImieAutora { get; set; }
        public string? NazwiskoAutora { get; set; }
        public int? Wiek { get; set; }
        public DateTime? DataUrodzenia { get; set; }
        public string? Opis { get; set; }

        public virtual ICollection<RecenzjaNaTematAutora> RecenzjaNaTematAutoras { get; set; }

        public virtual ICollection<Ksiazka> IdKsiazkas { get; set; }
    }
}
