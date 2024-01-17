using System;
using System.Collections.Generic;

namespace Bazowane.Models
{
    public partial class Polecenium
    {
        public decimal IdPolecenia { get; set; }
        public decimal IdZnajomosci { get; set; }
        public decimal IdKsiazka { get; set; }
        public string? TrescPolecenia { get; set; }
        public DateTime? DataPolecenia { get; set; }

        public virtual Ksiazka IdKsiazkaNavigation { get; set; } = null!;
        public virtual Znajomi IdZnajomosciNavigation { get; set; } = null!;
    }
}
