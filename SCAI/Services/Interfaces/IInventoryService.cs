#nullable enable
using SCAI.Models;

namespace SCAI.Services.Interfaces
{
    public interface IInventoryService
    {
        Task<List<Item>> GetAccessibleItemsAsync(string userRole);
        Task<Item?> GetItemByIdAsync(int id, string userRole);
        Task<List<Item>> SearchItemsByNameAsync(string name, string userRole);
        Task<(bool Success, string Message, Item? Item)> CreateItemAsync(Item item, string userRole);
        Task<(bool Success, string Message)> UpdateItemAsync(int id, Item updatedItem, string userRole);
        Task<(bool Success, string Message)> DeleteItemAsync(int id, string userRole);
    }
}