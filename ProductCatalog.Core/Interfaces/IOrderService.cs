using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCatalog.Core.DTOs.PlaceOrder;
using ProductCatalog.Core.Enums;

namespace ProductCatalog.Core.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResultDto> PlaceOrderAsync(PlaceOrderDto orderDto);
        Task<OrderDto?> GetOrderByIdAsync(int id);
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto?> UpdateOrderStatusAsync(int orderId, OrderStatus status);
    }
}
