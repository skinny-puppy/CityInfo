using CityInfo.API.Entities;
using CityInfo.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using City = CityInfo.API.Entities.City;
using Country = CityInfo.API.Entities.Country;

namespace CityInfo.API.DbContexts
{
    public class CityInfoContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<PointOfInterest> PointsOfInterest { get; set; }

        // povezivanje preko constructora
        public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasData(
                new Country("United States")
                {
                    Id = 1,
                    Description = "TODO 1",
                },
                new Country("Belgium")
                {
                    Id = 2,
                    Description = "TODO 2",
                },
               new Country("France")
                {
                    Id = 3,
                    Description = "TODO 3",
                });

            modelBuilder.Entity<City>().HasData(
                new City("New York City")
                {
                    Id = 1,
                    CountryId = 1,
                    Description = "The one with that big park.",
                },
                new City("Antwerp")
                {
                    Id = 2,
                    CountryId = 2,
                    Description = "The one with the cathedral that was never really finished.",
                },
                new City("Paris")
                {
                    Id = 3,
                    CountryId = 3,
                    Description = "The one with that big tower.",
                });

            modelBuilder.Entity<PointOfInterest>().HasData(
                new PointOfInterest("Central Park")
                {
                    Id = 1,
                    CityId = 1,
                    Description = "The most visited urban park in the United States."
                },
                new PointOfInterest("Empire State Building")
                {
                    Id = 2,
                    CityId = 1,
                    Description = "A 102-story skyscraper located in Midtown Manhattan."
                },

                new PointOfInterest("Cathedral of Our Lady")
                {
                    Id = 3,
                    CityId = 2,
                    Description = "A Gothic style cathedral, concieved by architects Jan and Piete."
                },
                new PointOfInterest("Antwerp Central Station")
                {
                    Id = 4,
                    CityId = 2,
                    Description = "The finest example of railway architecture in Belgium."
                },

                new PointOfInterest("Eiffel Tower")
                {
                    Id = 5,
                    CityId = 3,
                    Description = "A wrought iron lattice tower on the Champ de Mars."
                },
                new PointOfInterest("The Louvre")
                {
                    Id = 6,
                    CityId = 3,
                    Description = "The world's largest museum"
                }
                );
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<CityInfo.API.Models.CountryDto>? CountryDto { get; set; }



        //jedan nacin povezivanja sa bazom
        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("connectionstring");
            base.OnConfiguring(optionsBuilder);
        }
        */
    }
}
