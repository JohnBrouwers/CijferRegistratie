using CijferRegistratie.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CijferRegistratie.Data
{
    public class CijferRegistratieDbContext: DbContext
    {
        public DbSet<Vak> Vakken { get; set; }
        public DbSet<Poging> Pogingen { get; set; }

        public CijferRegistratieDbContext(DbContextOptions<CijferRegistratieDbContext> options): base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vak>().HasData(
                new Vak { 
                    Id = 1, 
                    Naam = "Server", 
                    EC = 4 },
                new Vak { 
                    Id = 2, 
                    Naam = "C#", 
                    EC = 4 },
                new Vak { 
                    Id = 3, 
                    Naam = "Databases", 
                    EC = 3 },
                new Vak { 
                    Id = 4, 
                    Naam = "UML", 
                    EC = 3 },
                new Vak { 
                    Id = 5, 
                    Naam = "KBS", 
                    EC = 9 }                
                );
        }
    }
}
