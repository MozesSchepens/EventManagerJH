using Microsoft.EntityFrameworkCore;
using EventManagerJH.Models;

namespace EventManagerJH.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Evenement> Evenementen { get; set; }
        public DbSet<TodoItem> ToDoItems { get; set; }
        public DbSet<Shift> Shiften { get; set; }
        public DbSet<Boodschap> Boodschappen { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
        }
    }
}
