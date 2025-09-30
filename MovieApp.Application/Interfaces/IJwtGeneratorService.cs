using MovieApp.Domain;

namespace MovieApp.Application.Interfaces
{
    public interface IJwtGeneratorService
    {
        string CreateToken(User user);
    }
}
