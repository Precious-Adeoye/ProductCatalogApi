using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCatalog.Core.Enums;

namespace ProductCatalog.Core.Entities
{
    [Table("Orders")]
    public class Order : BaseEntity
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "Order Number")]
        public string OrderNumber { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        [DataType(DataType.Currency)]
        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }

        [Required]
        [Display(Name = "Order Status")]
        public OrderStatus Status { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(200)]
        [Display(Name = "Customer Email")]
        public string CustomerEmail { get; set; } = string.Empty;

        [Required]
        [StringLength(200, MinimumLength = 2)]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; } = string.Empty;

        // Navigation property
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }
}
