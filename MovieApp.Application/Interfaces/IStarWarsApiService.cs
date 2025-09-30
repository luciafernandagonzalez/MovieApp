using MovieApp.Domain;

namespace MovieApp.Application.Interfaces
{
    public interface IStarWarsApiService
    {
        Task<Movie> GetMovie(int filmId);
        Task<List<Movie>> GetAllMovies();
    }
}
