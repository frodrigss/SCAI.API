using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SCAI.Infrastructure;
using SCAI.Models;
using SCAI.Models.Dtos;
using SCAI.Services.Interfaces;
using System.Security.Claims;

namespace SCAI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InventoryController(IInventoryService inventoryService) : ControllerBase
    {
        [HttpGet("items")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<Item>>> GetItems()
        {
            var userRole = User.FindFirstValue(ClaimTypes.Role) ?? RoleDefinitions.Trooper;
            var items = await inventoryService.GetAccessibleItemsAsync(userRole);
            return Ok(items);
        }

        [HttpGet("items/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Item>> GetItemById(int id)
        {
            var userRole = User.FindFirstValue(ClaimTypes.Role) ?? RoleDefinitions.Trooper;
            var item = await inventoryService.GetItemByIdAsync(id, userRole);
            if (item == null)
            {
                return NotFound(new { message = "Item não encontrado ou acesso negado" });
            }
            return Ok(item);
        }

        [HttpGet("items/search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Item>>> SearchItemsByName(string name)
        {
            var userRole = User.FindFirstValue(ClaimTypes.Role) ?? RoleDefinitions.Trooper;
            var items = await inventoryService.SearchItemsByNameAsync(name, userRole);
            if (items == null || items.Count == 0)
            {
                return NotFound(new { message = "Item não encontrado ou acesso negado" });
            }
            return Ok(items);
        }

        [HttpPost("items")]
        [Authorize(Roles = "Sith")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CreateItem(CreateItemDto request)
        {
            var userRole = User.FindFirstValue(ClaimTypes.Role) ?? RoleDefinitions.Trooper;

            var item = new Item
            {
                Name = request.Name,
                Description = request.Description,
                Quantity = request.Quantity,
                MinimalRoleLevel = request.MinimalRoleLevel
            };

            var result = await inventoryService.CreateItemAsync(item, userRole);
            if (!result.Success)
            {
                return BadRequest(new { message = result.Message });
            }

            return CreatedAtAction(nameof(GetItemById), new { id = result.Item!.Id }, result.Item);
        }

        [HttpPut("items/{id}")]
        [Authorize(Roles = "Sith,Commander")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateItem(int id, CreateItemDto request)
        {
            var userRole = User.FindFirstValue(ClaimTypes.Role) ?? RoleDefinitions.Trooper;

            var updatedItem = new Item
            {
                Name = request.Name,
                Description = request.Description,
                Quantity = request.Quantity,
                MinimalRoleLevel = request.MinimalRoleLevel
            };

            var result = await inventoryService.UpdateItemAsync(id, updatedItem, userRole);
            if (!result.Success)
            {
                if (result.Message.Contains("não encontrado", StringComparison.OrdinalIgnoreCase))
                {
                    return NotFound(new { message = result.Message });
                }

                return BadRequest(new { message = result.Message });
            }

            return Ok(new { message = result.Message });
        }

        [HttpDelete("items/{id}")]
        [Authorize(Roles = "Sith")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var userRole = User.FindFirstValue(ClaimTypes.Role) ?? RoleDefinitions.Trooper;

            var result = await inventoryService.DeleteItemAsync(id, userRole);
            if (!result.Success)
            {
                if (result.Message.Contains("não encontrado", StringComparison.OrdinalIgnoreCase))
                {
                    return NotFound(new { message = result.Message });
                }

                return BadRequest(new { message = result.Message });
            }

            return Ok(new { message = result.Message });
        }

    }

}