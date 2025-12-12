using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Application.DTOs.OrderDtos
{
    public class PlaceOrderDto
    {
        [Required, EmailAddress] public string CustomerEmail { get; set; } = string.Empty;
        [Required] public string CustomerName { get; set; } = string.Empty;
        [Required, MinLength(1)] public List<OrderItemDto> Items { get; set; } = new();
    }
}
