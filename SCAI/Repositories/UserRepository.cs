#nullable enable
using Microsoft.EntityFrameworkCore;
using SCAI.Infrastructure;
using SCAI.Models;
using SCAI.Repositories.Interfaces;
using System.Threading.Tasks;

namespace SCAI.Repositories
{
    public class UserRepository(ApplicationDbContext context) : IUserRepository
    {
        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task AddUserAsync(User user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            return await context.Users.AnyAsync(u => u.Username == username);
        }

        public async Task UpdateUserAsync(User user)
        {
            context.Users.Update(user);
            await context.SaveChangesAsync();
        }
    }
}
