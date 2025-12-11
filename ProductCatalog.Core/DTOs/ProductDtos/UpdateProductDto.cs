using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Core.DTOs.ProductDtos
{
    public class UpdateProductDto
    {
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Product name must be between 3 and 200 characters")]
        public string? Name { get; set; }
        
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }
        
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal? Price { get; set; }
        
        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity cannot be negative")]
        public int? StockQuantity { get; set; }
    }
}
