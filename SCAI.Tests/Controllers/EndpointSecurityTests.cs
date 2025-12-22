using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SCAI.Controllers;
using System.Reflection;
using Xunit;

namespace SCAI.Tests.Controllers
{
    public class EndpointSecurityTests
    {
        [Theory]
        [InlineData(nameof(InventoryController.CreateItem), "Sith")]
        [InlineData(nameof(InventoryController.DeleteItem), "Sith")]
        [InlineData(nameof(InventoryController.UpdateItem), "Sith,Commander")]
        public void Endpoint_ShouldHaveCorrectRoleAuthorization(string methodName, string expectedRoles)
        {
            // Arrange
            var methodInfo = typeof(InventoryController).GetMethod(methodName);
            Assert.NotNull(methodInfo);

            // Act
            var authorizeAttribute = methodInfo.GetCustomAttribute<AuthorizeAttribute>();

            // Assert
            Assert.NotNull(authorizeAttribute);
            Assert.Equal(expectedRoles, authorizeAttribute.Roles);
        }

        [Fact]
        public void InventoryController_ShouldRequireAuthentication()
        {
            // Arrange
            var type = typeof(InventoryController);

            // Act
            var authorizeAttribute = type.GetCustomAttribute<AuthorizeAttribute>();

            // Assert
            Assert.NotNull(authorizeAttribute); // Garante que o controller todo pede login
        }
    }
}
