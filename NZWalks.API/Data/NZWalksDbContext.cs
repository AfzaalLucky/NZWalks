using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {

        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> options) : base(options)
        {
            //
        }

        // Convert Domain Model Into Database
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Difficulty
            var difficulties = new List<Difficulty>()
            {
                new Difficulty
                {
                    Id = Guid.Parse("326d922b-ebb0-4c19-8829-462a016e2621"),
                    Name = "Low",
                },
                new Difficulty
                {
                    Id = Guid.Parse("876cabf2-b5c1-456e-b130-5b9c37520ce9"),
                    Name = "Medium",
                },
                new Difficulty
                {
                    Id = Guid.Parse("0640a92b-4c27-49b1-a2cf-57e221e087f2"),
                    Name = "High",
                },
            };

            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            // Region
            var regions = new List<Region>()
            {
                new Region
                {
                    Id = Guid.Parse("def50004-2102-4d74-8f90-fd2f51c04d39"),
                    Code = "DXB-R",
                    Name = "Dubai Region",
                    RegionImageUrl = "dubai-region-image-url.jpg",
                },
                new Region
                {
                    Id = Guid.Parse("fe21b1d4-80f7-4c28-b7db-85899a718e21"),
                    Code = "ADB-R",
                    Name = "Abu Dhabi",
                    RegionImageUrl = "abu-dhabi-region-image-url.jpg",
                },
                new Region
                {
                    Id = Guid.Parse("8b315288-64a6-442b-aa10-2da99acffe3e"),
                    Code = "SHR-R",
                    Name = "Sharjah",
                    RegionImageUrl = "sharjah-region-image-url.jpg",
                },
            };

            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
