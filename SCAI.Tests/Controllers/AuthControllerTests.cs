using Microsoft.AspNetCore.Mvc;
using Moq;
using SCAI.Controllers;
using SCAI.Models.Dtos;
using SCAI.Services;
using Xunit;

namespace SCAI.Tests.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> _mockAuthService;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _mockAuthService = new Mock<IAuthService>();
            _controller = new AuthController(_mockAuthService.Object);
        }

        [Fact]
        public async Task Register_ShouldReturnCreated_WhenSuccessful()
        {
            // Arrange
            var dto = new RegisterDto { Username = "User", Password = "Pass" };
            _mockAuthService.Setup(s => s.RegisterAsync(dto))
                .ReturnsAsync((true, "Success"));

            // Act
            var result = await _controller.Register(dto);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result);
            Assert.Equal(201, createdResult.StatusCode);
        }

        [Fact]
        public async Task Register_ShouldReturnBadRequest_WhenFailed()
        {
            // Arrange
            var dto = new RegisterDto { Username = "User", Password = "Pass" };
            _mockAuthService.Setup(s => s.RegisterAsync(dto))
                .ReturnsAsync((false, "Error"));

            // Act
            var result = await _controller.Register(dto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task Login_ShouldReturnOk_WhenSuccessful()
        {
            // Arrange
            var dto = new LoginDto { Username = "User", Password = "Pass" };
            _mockAuthService.Setup(s => s.LoginAsync(dto))
                .ReturnsAsync((true, "Token123", "Success"));

            // Act
            var result = await _controller.Login(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task Login_ShouldReturnUnauthorized_WhenFailed()
        {
            // Arrange
            var dto = new LoginDto { Username = "User", Password = "Pass" };
            _mockAuthService.Setup(s => s.LoginAsync(dto))
                .ReturnsAsync((false, null, "Error"));

            // Act
            var result = await _controller.Login(dto);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal(401, unauthorizedResult.StatusCode);
        }
    }
}
