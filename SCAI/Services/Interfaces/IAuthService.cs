#nullable enable
using SCAI.Models.Dtos;

namespace SCAI.Services
{
    public interface IAuthService
    {
        Task<(bool Success, string Message)> RegisterAsync(RegisterDto dto);
        Task<(bool Success, string? Token, string Message)> LoginAsync(LoginDto dto);
    }
}
