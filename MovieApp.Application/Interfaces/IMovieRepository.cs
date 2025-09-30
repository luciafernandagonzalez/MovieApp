using MovieApp.Domain;

namespace MovieApp.Application.Interfaces
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAll();
        Task<Movie> GetById(int id);
        Task<Movie> Add(Movie movie);
        Task Update(Movie movie);
        Task Delete(int id);
    }
}
