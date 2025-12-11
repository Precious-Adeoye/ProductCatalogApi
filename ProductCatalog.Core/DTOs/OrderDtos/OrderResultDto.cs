using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Core.DTOs.PlaceOrder
{
    public class OrderResultDto
    {
        public bool Success { get; set; }
        public int? OrderId { get; set; }
        public string? OrderNumber { get; set; }
        public decimal? TotalAmount { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string>? Errors { get; set; }
    }
}
