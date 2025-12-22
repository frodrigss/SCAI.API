using Moq;
using SCAI.Infrastructure;
using SCAI.Models;
using SCAI.Repositories.Interfaces;
using SCAI.Services;
using Xunit;

namespace SCAI.Tests.Services
{
    public class InventoryServiceTests
    {
        private readonly Mock<IItemRepository> _mockItemRepository;
        private readonly InventoryService _inventoryService;

        public InventoryServiceTests()
        {
            _mockItemRepository = new Mock<IItemRepository>();
            _inventoryService = new InventoryService(_mockItemRepository.Object);
        }

        [Fact]
        public async Task GetAccessibleItemsAsync_ShouldReturnItems_WhenRoleIsTrooper()
        {
            // Arrange
            var role = RoleDefinitions.Trooper;
            var expectedItems = new List<Item> { new Item { Name = "Blaster", MinimalRoleLevel = 3 } };
            _mockItemRepository.Setup(repo => repo.GetAccessibleByRoleLevelAsync(RoleDefinitions.TrooperLevel))
                .ReturnsAsync(expectedItems);

            // Act
            var result = await _inventoryService.GetAccessibleItemsAsync(role);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Blaster", result[0].Name);
        }

        [Fact]
        public async Task GetItemByIdAsync_ShouldReturnItem_WhenUserHasAccess()
        {
            // Arrange
            var role = RoleDefinitions.Sith; // Level 1
            var item = new Item { Id = 1, Name = "Lightsaber", MinimalRoleLevel = 1 };
            _mockItemRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(item);

            // Act
            var result = await _inventoryService.GetItemByIdAsync(1, role);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Lightsaber", result.Name);
        }

        [Fact]
        public async Task GetItemByIdAsync_ShouldReturnNull_WhenUserDoesNotHaveAccess()
        {
            // Arrange
            var role = RoleDefinitions.Trooper; // Level 3
            var item = new Item { Id = 1, Name = "Lightsaber", MinimalRoleLevel = 1 };
            _mockItemRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(item);

            // Act
            var result = await _inventoryService.GetItemByIdAsync(1, role);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateItemAsync_ShouldCreateItem_WhenCalled()
        {
            // Arrange
            var role = RoleDefinitions.Sith;
            var newItem = new Item { Name = "New Item", MinimalRoleLevel = 2 };

            // Act
            var result = await _inventoryService.CreateItemAsync(newItem, role);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Item criado com sucesso", result.Message);
            _mockItemRepository.Verify(repo => repo.AddAsync(newItem), Times.Once);
        }

        [Fact]
        public async Task UpdateItemAsync_ShouldFail_WhenUserIsNotAuthorized()
        {
            // Arrange
            var role = RoleDefinitions.Trooper; // Level 3 (Cannot update)
            var item = new Item { Name = "Updated" };

            // Act
            var result = await _inventoryService.UpdateItemAsync(1, item, role);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Permissões insuficientes", result.Message);
        }

        [Fact]
        public async Task UpdateItemAsync_ShouldUpdate_WhenUserIsAuthorized()
        {
            // Arrange
            var role = RoleDefinitions.Commander; // Level 2
            var existingItem = new Item { Id = 1, Name = "Old", MinimalRoleLevel = 2 };
            var updatedItem = new Item { Name = "New", MinimalRoleLevel = 2 };

            _mockItemRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(existingItem);

            // Act
            var result = await _inventoryService.UpdateItemAsync(1, updatedItem, role);

            // Assert
            Assert.True(result.Success);
            _mockItemRepository.Verify(repo => repo.UpdateAsync(existingItem), Times.Once);
            Assert.Equal("New", existingItem.Name);
        }

        [Fact]
        public async Task DeleteItemAsync_ShouldFail_WhenUserIsNotSith()
        {
            // Arrange
            var role = RoleDefinitions.Commander; // Level 2

            // Act
            var result = await _inventoryService.DeleteItemAsync(1, role);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Apenas Sith podem excluir", result.Message);
        }

        [Fact]
        public async Task DeleteItemAsync_ShouldDelete_WhenUserIsSith()
        {
            // Arrange
            var role = RoleDefinitions.Sith; // Level 1
            var existingItem = new Item { Id = 1 };
            _mockItemRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(existingItem);

            // Act
            var result = await _inventoryService.DeleteItemAsync(1, role);

            // Assert
            Assert.True(result.Success);
            _mockItemRepository.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }
    }
}
