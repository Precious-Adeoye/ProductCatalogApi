using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Core.Entities
{
    [Table("OrderItems")]
    public class OrderItem : BaseEntity
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order? Order { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }

    }
}
