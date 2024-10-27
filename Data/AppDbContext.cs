using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EventManagerJH.Models;

namespace EventManagerJH.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Evenement> Evenementen { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<Shift> Shiften { get; set; }
        public DbSet<Boodschap> Boodschappen { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EventManagerDB;Trusted_Connection=True;");
        }
    }
}
