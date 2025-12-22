using SCAI.Models;

namespace SCAI.Repositories.Interfaces
{
    public interface IItemRepository
    {
        Task<List<Item>> GetAllAsync();
        Task<List<Item>> GetAccessibleByRoleLevelAsync(int userRoleLevel);
        Task<Item> GetByIdAsync(int id);
        Task<List<Item>> SearchByNameAsync(string name);
        Task AddAsync(Item item);
        Task UpdateAsync(Item item);
        Task DeleteAsync(int id);
    }
}
