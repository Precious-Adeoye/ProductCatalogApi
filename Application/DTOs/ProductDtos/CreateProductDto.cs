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
        [Required] public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        [Required] public decimal Price { get; set; }
        [Required] public int StockQuantity { get; set; }
        [Required] public string Sku { get; set; } = string.Empty;
    }
}

