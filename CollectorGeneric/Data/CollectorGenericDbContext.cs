using Microsoft.EntityFrameworkCore;
using CollectorGeneric.Entities;

namespace CollectorGeneric.Data
{
    public class CollectorGenericDbContext : DbContext
    {
        public DbSet<Numismatics> Numismatics => Set<Numismatics>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseInMemoryDatabase("StorageAppDb");
        }
    }
}
