using Microsoft.EntityFrameworkCore;
using EventManagerJH.Models;

namespace EventManagerJH.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Evenement> Evenementen { get; set; }
        public DbSet<TodoItem> ToDoItems { get; set; }
        public DbSet<Shift> Shiften { get; set; }
        public DbSet<Boodschap> Boodschappen { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public AppDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EventManagerDB;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
