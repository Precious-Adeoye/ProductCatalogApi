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
        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        [StringLength(1000)]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        [Display(Name = "Stock Quantity")]
        public int StockQuantity { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [RegularExpression(@"^[A-Z0-9-]+$", ErrorMessage = "SKU must contain only uppercase letters, numbers, and hyphens")]
        public string Sku { get; set; } = string.Empty;

        [Timestamp]
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();

        // Navigation property
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
