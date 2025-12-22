using Microsoft.EntityFrameworkCore;
using Moq;
using SCAI.Infrastructure.Interfaces;
using SCAI.Models;
using SCAI.Models.Dtos;
using SCAI.Repositories.Interfaces;
using SCAI.Services;
using Xunit;

namespace SCAI.Tests.Services
{
    public class AuthServiceTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IJwtHelper> _mockJwtHelper;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockJwtHelper = new Mock<IJwtHelper>();
            _authService = new AuthService(_mockUserRepository.Object, _mockJwtHelper.Object);
        }

        [Fact]
        public async Task RegisterAsync_ShouldReturnSuccess_WhenUserIsNew()
        {
            // Arrange
            var dto = new RegisterDto { Username = "FN-2187", Password = "password" };
            _mockUserRepository.Setup(repo => repo.AddUserAsync(It.IsAny<User>())).Returns(Task.CompletedTask);

            // Act
            var result = await _authService.RegisterAsync(dto);

            // Assert
            Assert.True(result.Success);
            Assert.Contains("registrado com sucesso", result.Message);
        }

        [Fact]
        public async Task RegisterAsync_ShouldReturnFailure_WhenUserAlreadyExists()
        {
            // Arrange
            var dto = new RegisterDto { Username = "FN-2187", Password = "password" };
            _mockUserRepository.Setup(repo => repo.AddUserAsync(It.IsAny<User>()))
                .ThrowsAsync(new DbUpdateException());

            // Act
            var result = await _authService.RegisterAsync(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Este Trooper já existe", result.Message);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnToken_WhenCredentialsAreCorrect()
        {
            // Arrange
            var dto = new LoginDto { Username = "FN-2187", Password = "password" };
            var user = new User
            {
                Username = "FN-2187",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("password"),
                Role = "Trooper"
            };

            _mockUserRepository.Setup(repo => repo.GetUserByUsernameAsync(dto.Username)).ReturnsAsync(user);
            _mockJwtHelper.Setup(jwt => jwt.GenerateToken(user)).Returns("valid_token");

            // Act
            var result = await _authService.LoginAsync(dto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("valid_token", result.Token);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnFailure_WhenPasswordIsIncorrect()
        {
            // Arrange
            var dto = new LoginDto { Username = "FN-2187", Password = "wrongpassword" };
            var user = new User
            {
                Username = "FN-2187",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("password"),
                Role = "Trooper"
            };

            _mockUserRepository.Setup(repo => repo.GetUserByUsernameAsync(dto.Username)).ReturnsAsync(user);

            // Act
            var result = await _authService.LoginAsync(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Null(result.Token);
            Assert.Contains("Credenciais inválidas", result.Message);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnFailure_WhenUserNotFound()
        {
            // Arrange
            var dto = new LoginDto { Username = "Unknown", Password = "password" };
            _mockUserRepository.Setup(repo => repo.GetUserByUsernameAsync(dto.Username)).ReturnsAsync((User?)null);

            // Act
            var result = await _authService.LoginAsync(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Credenciais inválidas", result.Message);
        }
    }
}
