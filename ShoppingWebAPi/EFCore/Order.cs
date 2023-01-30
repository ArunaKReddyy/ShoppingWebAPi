using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShoppingWebAPi.EFCore
{
    [Table("Order")]
    public class Order
    {
        [Key, Required]
        public int Id { get; set; }
        public virtual Product Product { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
