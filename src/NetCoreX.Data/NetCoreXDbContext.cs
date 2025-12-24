using Microsoft.EntityFrameworkCore;
using NetCoreX.Domain;

namespace NetCoreX.Data
{
    public class NetCoreXDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        public NetCoreXDbContext()
        {
            
        }

        public NetCoreXDbContext(DbContextOptions<NetCoreXDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlite("Data Source=AppData/NetCoreXDb.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Contact).Assembly);
        }
    }
}
