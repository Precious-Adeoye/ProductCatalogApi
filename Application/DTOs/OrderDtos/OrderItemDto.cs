using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Core.DTOs.PlaceOrderDto
{
    public class OrderItemDto
    {
        [Required] public int ProductId { get; set; }
        [Required] public int Quantity { get; set; } 
    }
}
