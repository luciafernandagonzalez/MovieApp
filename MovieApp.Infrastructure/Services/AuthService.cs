using MovieApp.Application.DTOs;
using MovieApp.Application.Interfaces;
using MovieApp.Domain;

namespace MovieApp.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtGeneratorService _jwtGenerator;

        public AuthService(IUserRepository userRepository, IJwtGeneratorService jwtGenerator)
        {
            _userRepository = userRepository;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<AuthResponseDto> Register(RegisterRequestDto request)
        {
            var existingUser = await _userRepository.GetByUsername(request.Username); 
            if (existingUser != null)
                throw new Exception("Username is already taken.");

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var newUser = new User
            {
                Username = request.Username,
                Password = passwordHash,
                Role = "Regular" //por defecto regular
            };

            var createdUser = await _userRepository.Add(newUser); 
            var token = _jwtGenerator.CreateToken(createdUser); 

            return new AuthResponseDto
            {
                Token = token,
                UserName = createdUser.Username,
                Role = createdUser.Role,
            };
        }

        public async Task<AuthResponseDto> Login(LoginRequestDto request)
        {
            var user = await _userRepository.GetByUsername(request.Username);
            if (user == null)
                throw new UnauthorizedAccessException("Invalid credentials");

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
            if (!isPasswordValid)
                throw new UnauthorizedAccessException("Invalid credentials");

            var token = _jwtGenerator.CreateToken(user);
            return new AuthResponseDto
            {
                Token = token,
                UserName = user.Username,
                Role = user.Role
            };
        }
    }
}
