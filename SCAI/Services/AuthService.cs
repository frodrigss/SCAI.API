#nullable enable
using Microsoft.EntityFrameworkCore;
using SCAI.Infrastructure;
using SCAI.Infrastructure.Interfaces;
using SCAI.Models;
using SCAI.Models.Dtos;
using SCAI.Repositories.Interfaces;

namespace SCAI.Services
{
    public class AuthService(IUserRepository userRepository, IJwtHelper jwt) : IAuthService
    {
        public async Task<(bool Success, string Message)> RegisterAsync(RegisterDto dto)
        {
            var user = new User
            {
                Username = dto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = RoleDefinitions.Trooper,
            };

            try
            {
                await userRepository.AddUserAsync(user);
            }
            catch (DbUpdateException)
            {
                return (false, "Este Trooper já existe. Lord Vader exige identificação única para seus subordinados.");
            }

            return (true, $"Recruta {dto.Username} registrado com sucesso. Bem-vindo ao Império Galáctico.");
        }

        public async Task<(bool Success, string? Token, string Message)> LoginAsync(LoginDto dto)
        {
            var user = await userRepository.GetUserByUsernameAsync(dto.Username);
            
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                return (false, null, "Credenciais inválidas. A Força não é forte neste login. Verifique se o nome de usuário e a senha estão corretos.");
            }

            var token = jwt.GenerateToken(user);

            return (true, token, $"Acesso autorizado para o Trooper {user.Username}. Bem-vindo de volta. Sirva bem ao Império.");
        }
    }
}