using Microsoft.EntityFrameworkCore;
using EventManagerJH.Models;
using System;

namespace EventManagerJH.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Evenement> Evenementen { get; set; }
        public DbSet<TodoItem> ToDoItems { get; set; }
        public DbSet<Boodschap> Boodschappen { get; set; }
        public DbSet<Shift> Shiften { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data voor testdoeleinden
            modelBuilder.Entity<Evenement>().HasData(
                new Evenement
                {
                    EvenementID = 1,
                    Titel = "Koerrock",
                    Datum = new DateTime(2024, 9, 27),
                    Locatie = "Jeugdhuis",
                    Beschrijving = "Groot feest"
                },
                new Evenement
                {
                    EvenementID = 2,
                    Titel = "Verjaardag Casi",
                    Datum = new DateTime(2024, 12, 6),
                    Locatie = "Binnen",
                    Beschrijving = "Privé evenement"
                }
            );
        }
    }
}
