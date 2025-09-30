using MovieApp.Application.DTOs;
using MovieApp.Domain;

namespace MovieApp.Application.Interfaces
{
    public interface IMovieService
    {
        Task<List<Movie>> GetAll();
        Task<Movie> GetById(int id);
        Task<Movie> Create(MovieCreateRequestDto request);
        Task<Movie> Update(int id, MovieUpdateRequestDto request);
        Task<bool> Delete(int id);
    }
}
