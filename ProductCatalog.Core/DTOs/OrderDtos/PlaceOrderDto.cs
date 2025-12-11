using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCatalog.Core.DTOs.PlaceOrderDto;

namespace ProductCatalog.Core.DTOs.PlaceOrder
{
    public class PlaceOrderDto
    {
        [Required(ErrorMessage = "Customer email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(200, ErrorMessage = "Email cannot exceed 200 characters")]
        public string CustomerEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Customer name is required")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Customer name must be between 2 and 200 characters")]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Order items are required")]
        [MinLength(1, ErrorMessage = "At least one item is required")]
        public List<OrderItemDto> Items { get; set; } = new();
    }
}
