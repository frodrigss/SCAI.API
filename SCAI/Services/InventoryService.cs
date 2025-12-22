#nullable enable
using SCAI.Infrastructure;
using SCAI.Models;
using SCAI.Repositories;
using SCAI.Repositories.Interfaces;
using SCAI.Services.Interfaces;

namespace SCAI.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IItemRepository _itemRepository;

        public InventoryService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<List<Item>> GetAccessibleItemsAsync(string userRole)
        {
            var userLevel = RoleDefinitions.GetRoleLevel(userRole);
            return await _itemRepository.GetAccessibleByRoleLevelAsync(userLevel);
        }

        public async Task<Item?> GetItemByIdAsync(int id, string userRole)
        {
            var item = await _itemRepository.GetByIdAsync(id);
            if (item == null) return null;

            var userLevel = RoleDefinitions.GetRoleLevel(userRole);
            return userLevel <= item.MinimalRoleLevel ? item : null;
        }

        public async Task<List<Item>> SearchItemsByNameAsync(string name, string userRole)
        {
            var items = await _itemRepository.SearchByNameAsync(name);
            var userLevel = RoleDefinitions.GetRoleLevel(userRole);

            return items.Where(i => userLevel <= i.MinimalRoleLevel).ToList();
        }

        public async Task<(bool Success, string Message, Item? Item)> CreateItemAsync(Item item, string userRole)
        {
            await _itemRepository.AddAsync(item);
            return (true, "Item criado com sucesso", item);
        }

        public async Task<(bool Success, string Message)> UpdateItemAsync(int id, Item updatedItem, string userRole)
        {
            var userLevel = RoleDefinitions.GetRoleLevel(userRole);
            if (userLevel > RoleDefinitions.CommanderLevel)
            {
                return (false, "Permissões insuficientes para atualizar itens");
            }

            var existingItem = await _itemRepository.GetByIdAsync(id);
            if (existingItem == null)
            {
                return (false, "Item não encontrado");
            }

            if (existingItem.MinimalRoleLevel < userLevel)
            {
                return (false, "Não é possível atualizar itens com nível de permissão superior ao seu cargo");
            }

            existingItem.Name = updatedItem.Name;
            existingItem.Description = updatedItem.Description;
            existingItem.Quantity = updatedItem.Quantity;
            existingItem.MinimalRoleLevel = updatedItem.MinimalRoleLevel;

            await _itemRepository.UpdateAsync(existingItem);
            return (true, "Item atualizado com sucesso");
        }

        public async Task<(bool Success, string Message)> DeleteItemAsync(int id, string userRole)
        {
            var userLevel = RoleDefinitions.GetRoleLevel(userRole);

            if (userLevel > RoleDefinitions.SithLevel)
            {
                return (false, "Permissões insuficientes para excluir itens. Apenas Sith podem excluir.");
            }

            var existingItem = await _itemRepository.GetByIdAsync(id);
            if (existingItem == null)
            {
                return (false, "Item não encontrado");
            }

            await _itemRepository.DeleteAsync(id);
            return (true, "Item excluído com sucesso");
        }
    }
}
