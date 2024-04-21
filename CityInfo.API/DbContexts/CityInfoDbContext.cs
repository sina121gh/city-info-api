using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.DbContexts
{
    public class CityInfoDbContext : DbContext
    {

        public CityInfoDbContext
            (DbContextOptions<CityInfoDbContext> options) : base(options)
        {

        }


        public DbSet<City> Cities { get; set; }
        public DbSet<CitySight> CitySights { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Seed Data

            #region Cities

            modelBuilder.Entity<City>()
                .HasData(
                new City("Tehran")
                {
                    Id = 1,
                    Description = "This is Tehran",
                },

                new City("Kashan")
                {
                    Id = 2,
                    Description = "This is Kashan",
                },

                new City("Tabriz")
                {
                    Id = 3,
                    Description = "This is Tabriz",
                }
                );

            #endregion

            #region Sights

            modelBuilder.Entity<CitySight>()
                .HasData(
                new CitySight("Academy Barnamenevisan")
                {
                    Id = 1,
                    CityId = 1,
                    Description = "Tel 02188454816",
                },

                new CitySight("Shemiran")
                {
                    Id = 2,
                    CityId = 1,
                    Description = "This is Shemiran",
                },

                new CitySight("Meydane ToopKhoone")
                {
                    Id = 3,
                    CityId = 1,
                    Description = "This is ToopKhoone",
                }
                );

            #endregion

            #endregion

            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
