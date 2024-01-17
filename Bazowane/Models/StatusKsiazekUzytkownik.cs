using System;
using System.Collections.Generic;

namespace Bazowane.Models
{
    public partial class StatusKsiazekUzytkownik
    {
        public decimal IdStatusu { get; set; }
        public decimal IdUzytkownik { get; set; }
        public decimal IdKsiazka { get; set; }

        public virtual Ksiazka IdKsiazkaNavigation { get; set; } = null!;
        public virtual Status IdStatusuNavigation { get; set; } = null!;
        public virtual Uzytkownik IdUzytkownikNavigation { get; set; } = null!;
    }
}
