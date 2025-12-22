#nullable enable
using SCAI.Models;
using System.Threading.Tasks;

namespace SCAI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUsernameAsync(string username);
        Task AddUserAsync(User user);
        Task<bool> UserExistsAsync(string username);
        Task UpdateUserAsync(User user);
    }
}
