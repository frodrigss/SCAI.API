using System.ComponentModel.DataAnnotations;

namespace SCAI.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int MinimalRoleLevel { get; set; } = 3;
    }
}
