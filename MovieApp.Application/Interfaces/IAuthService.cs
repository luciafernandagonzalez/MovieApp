using MovieApp.Application.DTOs;

namespace MovieApp.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> Register(RegisterRequestDto request);
        Task<AuthResponseDto> Login(LoginRequestDto request); 

    }
}
