using System.ComponentModel.DataAnnotations;

namespace SCAI.Models.Dtos
{
    public class LoginDto
    {
        [Required]
        [MinLength(6, ErrorMessage = "Lord Vader exige que Troopers tenham pelo menos 6 caracteres em seus nomes ultra-unicos do Império.")]
        public string Username { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Lord Vader exige que Troopers tenham pelo menos 8 caracteres em suas senhas ultra-seguras do Império.")]
        public string Password { get; set; }
    }
}