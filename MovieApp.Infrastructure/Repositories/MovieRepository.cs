using MovieApp.Application.Interfaces;
using MovieApp.Domain;
using MovieApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MovieApp.Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    { 
        private readonly MovieAppDbContext _context;
        
        public MovieRepository(MovieAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie>> GetAll()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<Movie> GetById(int id)
        {
            return await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Movie> Add(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync(); 
            return movie;
        }

        public async Task Update(Movie movie)
        {
            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
            }
        }
    }
}
