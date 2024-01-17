using System;
using System.Collections.Generic;

namespace Bazowane.Models
{
    public partial class Ocena
    {
        public decimal IdOceny { get; set; }
        public decimal? IdKsiazka { get; set; }
        public decimal? IdUzytkownik { get; set; }
        public int? Ocena1 { get; set; }
        public string? Opinia { get; set; }
        public DateTime? DataOceny { get; set; }

        public virtual Ksiazka? IdKsiazkaNavigation { get; set; }
        public virtual Uzytkownik? IdUzytkownikNavigation { get; set; }
    }
}
