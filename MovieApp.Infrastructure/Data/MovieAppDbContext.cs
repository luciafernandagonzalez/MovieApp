using Microsoft.EntityFrameworkCore;
using MovieApp.Domain;

namespace MovieApp.Infrastructure.Data
{
    public class MovieAppDbContext : DbContext
    {
        public MovieAppDbContext(DbContextOptions<MovieAppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }

    }
}
