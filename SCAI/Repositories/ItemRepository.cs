using Microsoft.EntityFrameworkCore;
using SCAI.Infrastructure;
using SCAI.Models;
using SCAI.Repositories.Interfaces;
using System.Linq;

namespace SCAI.Repositories
{
    public class ItemRepository(ApplicationDbContext context) : IItemRepository
    {

        public async Task<List<Item>> GetAllAsync()
        {
            return await context.Items.ToListAsync();
        }

        public async Task<List<Item>> GetAccessibleByRoleLevelAsync(int userRoleLevel)
        {
            return await context.Items
                .Where(i => i.MinimalRoleLevel >= userRoleLevel)
                .OrderBy(i => i.Id)
                .ToListAsync();
        }

        public async Task<Item> GetByIdAsync(int id)
        {
            return await context.Items.FindAsync(id);
        }

        public async Task<List<Item>> SearchByNameAsync(string name)
        {
            return await context.Items
                .Where(i => i.Name.Contains(name))
                .ToListAsync();
        }

        public async Task AddAsync(Item item)
        {
            await context.Items.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Item item)
        {
            context.Items.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await context.Items.FindAsync(id);
            if (item != null)
            {
                context.Items.Remove(item);
                await context.SaveChangesAsync();
            }
        }
    }
}
