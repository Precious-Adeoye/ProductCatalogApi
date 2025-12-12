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
        [Required, StringLength(50)]
        public string OrderNumber { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        [Required, StringLength(200)]
        public string CustomerEmail { get; set; } = string.Empty;
        [Required, StringLength(200)]
        public string CustomerName { get; set; } = string.Empty;
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
