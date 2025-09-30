using Moq;
using MovieApp.Application.DTOs;
using MovieApp.Application.Interfaces;
using MovieApp.Domain;
using MovieApp.Infrastructure.Services;

namespace MovieApp.Tests
{
    public class AuthServiceTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IJwtGeneratorService> _mockJwtGenerator;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockJwtGenerator = new Mock<IJwtGeneratorService>();
            _authService = new AuthService(_mockUserRepository.Object, _mockJwtGenerator.Object);
        }

        //Test 1: Login
        [Fact]
        public async Task Login_ShouldReturnToken_WhenCredentialsAreValid()
        {
            var username = "testuser";
            var password = "validpassword";
            var hashedPass = BCrypt.Net.BCrypt.HashPassword(password);
            var expectedToken = "FAKE_JWT_TOKEN";

            var user = new User { Id = 1, Username = username, Password = hashedPass, Role = "Regular" };
            var request = new LoginRequestDto { Username = username, Password = password };

            _mockUserRepository.Setup(r => r.GetByUsername(username)).ReturnsAsync(user);
            _mockJwtGenerator.Setup(j => j.CreateToken(user)).Returns(expectedToken);

            //act
            var result = await _authService.Login(request);

            //assert
            Assert.Equal(expectedToken, result.Token);
            _mockUserRepository.Verify(r => r.GetByUsername(username), Times.Once());
            _mockJwtGenerator.Verify(j => j.CreateToken(user), Times.Once);
        }

        //Test 2: Login failed
        [Fact]
        public async Task LoginAsync_ShouldThrowUnauthorizedException_WhenPasswordIsInvalid()
        {
            var username = "testuser";
            var user = new User { Username = username, Password = BCrypt.Net.BCrypt.HashPassword("correct_password") };
            var request = new LoginRequestDto { Username = username, Password = "wrong_password" };

            _mockUserRepository.Setup(r => r.GetByUsername(username))
                               .ReturnsAsync(user);

            // act y assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _authService.Login(request));

            _mockJwtGenerator.Verify(j => j.CreateToken(It.IsAny<User>()), Times.Never);
        }
    }
}
