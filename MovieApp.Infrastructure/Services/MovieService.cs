using MovieApp.Application.DTOs;
using MovieApp.Application.Interfaces;
using MovieApp.Domain;

namespace MovieApp.Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<Movie> Create(MovieCreateRequestDto request)
        {
            var movie = new Movie();
            movie.Title = request.Title;
            movie.Director = request.Director;
            movie.Producer = request.Producer;

            await _movieRepository.Add(movie);
            return movie;
        }

        public async Task<bool> Delete(int id)
        {
            var movie = await _movieRepository.GetById(id);
            if (movie == null) return false;

            await _movieRepository.Delete(id);
            return true;
        }

        public async Task<List<Movie>> GetAll()
        {
            var movies = await _movieRepository.GetAll();
            return movies.ToList();
        }

        public async Task<Movie> GetById(int id)
        {
            var movie = await _movieRepository.GetById(id);
            return movie;
        }

        public async Task<Movie> Update(int id, MovieUpdateRequestDto request)
        {
            var movie = await _movieRepository.GetById(id);
            if (movie == null) return null;

            movie.Title = request.Title;
            movie.Director = request.Director;
            movie.Producer = request.Producer;

            await _movieRepository.Update(movie); 
            return movie;
        }
    }
}
