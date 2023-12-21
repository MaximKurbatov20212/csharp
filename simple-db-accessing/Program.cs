
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1
{
    public sealed class ApplicationContext : DbContext
    {
        public DbSet<ExperimentEntity> Experiments { get; set; } = null!;
        public DbSet<CardEntity> CardEntities { get; set; } = null!;

        public ApplicationContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("lab4.db");
        }
    }
}