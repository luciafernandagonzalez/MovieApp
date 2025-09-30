using MovieApp.Domain;

namespace MovieApp.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByUsername(string username);
        Task<User> Add(User user);
    }
}
