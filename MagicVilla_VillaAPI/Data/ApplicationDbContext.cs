using MagicVilla_VillaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Data
{
    //comments
    public class ApplicationDbContext:DbContext
    {
        //2:09:45
        public ApplicationDbContext(DbContextOptions options) 
            : base(options)
        {
        }

        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa
                {
                    Id=1,
                    Name = "Royal Villa",
                    Details = "This is the Royal Villa",
                    Rate = 200.0,
                    Sqft = 550,
                    Occupancy = 4,
                    ImageUrl = "https://dotnetmastery.com/bluevillaimages/villa3.jpg",
                    Amenity = "",
                    CreatedDate = DateTime.Now,
                },
                new Villa
                {
                    Id=2,
                    Name = "Premium Pool Villa",
                    Details = "This is the Premium Pool Villa",
                    Rate = 300.0,
                    Sqft = 550,
                    Occupancy = 4,
                    ImageUrl = "https://dotnetmastery.com/bluevillaimages/villa1.jpg",
                    Amenity = "",
                    CreatedDate = DateTime.Now,
                },
                new Villa
                {
                    Id = 3,
                    Name = "Villa 3",
                    Details = "This is 3 Villa",
                    Rate = 400.0,
                    Sqft = 550,
                    Occupancy = 4,
                    ImageUrl = "https://dotnetmastery.com/bluevillaimages/villa2.jpg",
                    Amenity = "",
                    CreatedDate = DateTime.Now,
                },
                new Villa
                {
                    Id = 4,
                    Name = "Villa 4",
                    Details = "This is 4 Villa",
                    Rate = 500.0,
                    Sqft = 550,
                    Occupancy = 4,
                    ImageUrl = "https://dotnetmastery.com/bluevillaimages/villa4.jpg",
                    Amenity = "",
                    CreatedDate = DateTime.Now,
                },
                new Villa
                {
                    Id = 5,
                    Name = "Villa 5",
                    Details = "This is 5 Villa",
                    Rate = 600.0,
                    Sqft = 550,
                    Occupancy = 4,
                    ImageUrl = "https://dotnetmastery.com/bluevillaimages/villa5.jpg",
                    Amenity = "",
                    CreatedDate = DateTime.Now,
                }
            );
        }

    }
}
