using System.ComponentModel.DataAnnotations;

namespace SCAI.Models.Dtos
{
    public class CreateItemDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "A descrição do item deve ter pelo menos 10 caracteres para atender aos padrões rigorosos do Império.")]
        [MaxLength(200, ErrorMessage = "A descrição do item não pode exceder 200 caracteres, conforme as diretrizes do Império.")]
        public string Description { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Range(1, 3, ErrorMessage = "O nível de função mínima deve ser entre 1 (Sith) e 3 (Trooper), conforme a hierarquia do Império.")]
        public int MinimalRoleLevel { get; set; }
    }
}