using System;
using System.Collections.Generic;

namespace Bazowane.Models
{
    public partial class Gatunek
    {
        public Gatunek()
        {
            IdKsiazkas = new HashSet<Ksiazka>();
        }

        public decimal IdGatunku { get; set; }
        public string? NazwaGatunku { get; set; }

        public virtual ICollection<Ksiazka> IdKsiazkas { get; set; }
    }
}
