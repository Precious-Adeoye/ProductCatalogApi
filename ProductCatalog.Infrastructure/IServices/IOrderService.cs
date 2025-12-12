using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCatalog.Application.DTOs.OrderDtos;

namespace ProductCatalog.Infrastructure.IServices
{
    public interface IOrderService
    {
        Task<OrderResultDto> PlaceOrderAsync(PlaceOrderDto orderDto);
        Task<OrderDto?> GetOrderByIdAsync(int id);
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
    }
}
