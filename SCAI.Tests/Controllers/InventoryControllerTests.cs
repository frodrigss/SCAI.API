using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SCAI.Controllers;
using SCAI.Infrastructure;
using SCAI.Models;
using SCAI.Models.Dtos;
using SCAI.Services.Interfaces;
using System.Security.Claims;
using Xunit;

namespace SCAI.Tests.Controllers
{
    public class InventoryControllerTests
    {
        private readonly Mock<IInventoryService> _mockInventoryService;
        private readonly InventoryController _controller;

        public InventoryControllerTests()
        {
            _mockInventoryService = new Mock<IInventoryService>();
            _controller = new InventoryController(_mockInventoryService.Object);
        }

        private void SetupUser(string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "TestUser"),
                new Claim(ClaimTypes.Role, role)
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = claimsPrincipal }
            };
        }

        [Fact]
        public async Task GetItems_ShouldReturnOk_WithItems()
        {
            // Arrange
            SetupUser(RoleDefinitions.Trooper);
            var items = new List<Item> { new Item { Name = "Blaster" } };
            _mockInventoryService.Setup(s => s.GetAccessibleItemsAsync(RoleDefinitions.Trooper))
                .ReturnsAsync(items);

            // Act
            var result = await _controller.GetItems();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnItems = Assert.IsType<List<Item>>(okResult.Value);
            Assert.Single(returnItems);
        }

        [Fact]
        public async Task GetItemById_ShouldReturnOk_WhenItemExists()
        {
            // Arrange
            SetupUser(RoleDefinitions.Sith);
            var item = new Item { Id = 1, Name = "Lightsaber" };
            _mockInventoryService.Setup(s => s.GetItemByIdAsync(1, RoleDefinitions.Sith))
                .ReturnsAsync(item);

            // Act
            var result = await _controller.GetItemById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnItem = Assert.IsType<Item>(okResult.Value);
            Assert.Equal("Lightsaber", returnItem.Name);
        }

        [Fact]
        public async Task GetItemById_ShouldReturnNotFound_WhenItemDoesNotExist()
        {
            // Arrange
            SetupUser(RoleDefinitions.Trooper);
            _mockInventoryService.Setup(s => s.GetItemByIdAsync(1, RoleDefinitions.Trooper))
                .ReturnsAsync((Item?)null);

            // Act
            var result = await _controller.GetItemById(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task CreateItem_ShouldReturnCreated_WhenSuccessful()
        {
            // Arrange
            SetupUser(RoleDefinitions.Sith);
            var dto = new CreateItemDto { Name = "New Item", Quantity = 10, MinimalRoleLevel = 1, Description = "A powerful item" };
            var createdItem = new Item { Id = 1, Name = "New Item" };

            _mockInventoryService.Setup(s => s.CreateItemAsync(It.IsAny<Item>(), RoleDefinitions.Sith))
                .ReturnsAsync((true, "Success", createdItem));

            // Act
            var result = await _controller.CreateItem(dto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(InventoryController.GetItemById), createdResult.ActionName);
            Assert.Equal(createdItem, createdResult.Value);
        }

        [Fact]
        public async Task CreateItem_ShouldReturnBadRequest_WhenServiceFails()
        {
            // Arrange
            SetupUser(RoleDefinitions.Sith);
            var dto = new CreateItemDto { Name = "New Item", Quantity = 10, MinimalRoleLevel = 1, Description = "A powerful item" };

            _mockInventoryService.Setup(s => s.CreateItemAsync(It.IsAny<Item>(), RoleDefinitions.Sith))
                .ReturnsAsync((false, "Error", null));

            // Act
            var result = await _controller.CreateItem(dto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.NotNull(badRequestResult.Value);
        }

        [Fact]
        public async Task UpdateItem_ShouldReturnOk_WhenSuccessful()
        {
            // Arrange
            SetupUser(RoleDefinitions.Commander);
            var dto = new CreateItemDto { Name = "Updated Item", Quantity = 5, MinimalRoleLevel = 2, Description = "Updated desc" };

            _mockInventoryService.Setup(s => s.UpdateItemAsync(1, It.IsAny<Item>(), RoleDefinitions.Commander))
                .ReturnsAsync((true, "Success"));

            // Act
            var result = await _controller.UpdateItem(1, dto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteItem_ShouldReturnOk_WhenSuccessful()
        {
            // Arrange
            SetupUser(RoleDefinitions.Sith);

            _mockInventoryService.Setup(s => s.DeleteItemAsync(1, RoleDefinitions.Sith))
                .ReturnsAsync((true, "Success"));

            // Act
            var result = await _controller.DeleteItem(1);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
