using SCAI.Models;

namespace SCAI.Infrastructure.Interfaces
{
    public interface IJwtHelper
    {
        string GenerateToken(User user);
    }
}
