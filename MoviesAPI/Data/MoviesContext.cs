using Microsoft.EntityFrameworkCore;

namespace Codeology_Tests.Data
{
    public class MoviesContext : DbContext
    {
        public MoviesContext(DbContextOptions<MoviesContext> options) : base(options)
        {
        }
        public DbSet<Movies> MoviesDB { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Set the database provider and connection string
            optionsBuilder.UseSqlServer("Server=localhost;Database=Movies;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False;");
        }
    }
}