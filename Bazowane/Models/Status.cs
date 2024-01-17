using System;
using System.Collections.Generic;

namespace Bazowane.Models
{
    public partial class Status
    {
        public Status()
        {
            Ksiazkas = new HashSet<Ksiazka>();
            StatusKsiazekUzytkowniks = new HashSet<StatusKsiazekUzytkownik>();
        }

        public decimal IdStatusu { get; set; }
        public string? StanStatusu { get; set; }

        public virtual ICollection<Ksiazka> Ksiazkas { get; set; }
        public virtual ICollection<StatusKsiazekUzytkownik> StatusKsiazekUzytkowniks { get; set; }
    }
}
