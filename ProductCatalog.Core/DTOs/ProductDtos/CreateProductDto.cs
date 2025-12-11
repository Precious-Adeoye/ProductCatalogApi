using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Core.DTOs.ProductDtos
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "Product name is required")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Product name must be between 3 and 200 characters")]
        public string Name { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stock quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity cannot be negative")]
        public int StockQuantity { get; set; }

        [Required(ErrorMessage = "SKU is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "SKU must be between 3 and 50 characters")]
        [RegularExpression(@"^[A-Z0-9-]+$", ErrorMessage = "SKU must contain only uppercase letters, numbers, and hyphens")]
        public string Sku { get; set; } = string.Empty;
    }
}

