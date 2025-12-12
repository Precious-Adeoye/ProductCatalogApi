using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Core.Entities
{
    [Table("Products")]
    public class Product : BaseEntity
    {
        [Required, StringLength(200)]
        public string Name { get; set; } = string.Empty;
        [StringLength(1000)]
        public string? Description { get; set; }
        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Required]
        public int StockQuantity { get; set; }
        [Required, StringLength(50)]
        public string Sku { get; set; } = string.Empty;
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
