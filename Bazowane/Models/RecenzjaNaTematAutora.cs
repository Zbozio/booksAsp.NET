using System;
using System.Collections.Generic;

namespace Bazowane.Models
{
    public partial class RecenzjaNaTematAutora
    {
        public decimal IdRecenzji { get; set; }
        public decimal IdAutora { get; set; }
        public decimal IdUzytkownik { get; set; }
        public int? OcenaAutora { get; set; }
        public string? RecenzjaTeskt { get; set; }
        public DateTime? DataRecenzji { get; set; }

        public virtual Autor IdAutoraNavigation { get; set; } = null!;
        public virtual Uzytkownik IdUzytkownikNavigation { get; set; } = null!;
    }
}
