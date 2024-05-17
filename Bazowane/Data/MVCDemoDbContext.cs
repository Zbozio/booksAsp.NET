using Bazowane.Models;
using Microsoft.EntityFrameworkCore;

namespace Bazowane.Data
{
    public class MVCDemoDbContext : DbContext
    {
        public MVCDemoDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet <Ksiazka> Ksiazka{ get; set; }
    }
}
