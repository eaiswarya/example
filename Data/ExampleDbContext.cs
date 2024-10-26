using example.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace example.API.Data
{
    public class ExampleDbContext:DbContext
    {
        public ExampleDbContext(DbContextOptions dbContextOptions):base(dbContextOptions) 
        { 

        }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }

        public DbSet<Walk> Walks { get; set; }




    }
}
