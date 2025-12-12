using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductCatalog.Application.DTOs.OrderDtos;
using ProductCatalog.Core.Contracts.Interface;
using ProductCatalog.Core.Entities;
using ProductCatalog.Core.Enums;
using ProductCatalog.DataAccess.Repositories.Interface;
using ProductCatalog.Infrastructure.IServices;

namespace ProductCatalog.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _uow;
        private readonly IOrderRepository _orderRepository;

        public OrderService(IUnitOfWork uow, IOrderRepository orderRepository)
        {
            _uow = uow;
            _orderRepository = orderRepository;
        }

        // ---------------------------------------------
        // PLACE ORDER (Transaction + Stock Protection)
        // ---------------------------------------------
        public async Task<OrderResultDto> PlaceOrderAsync(PlaceOrderDto orderDto)
        {
            await _uow.BeginTransactionAsync();

            try
            {
                if (orderDto.Items == null || !orderDto.Items.Any())
                {
                    return new OrderResultDto { Success = false, Message = "No items provided." };
                }

                // Get distinct product IDs from order request
                var productIds = orderDto.Items.Select(i => i.ProductId).Distinct().ToList();

                // Fetch products from DB
                var products = (await _uow.ProductRepository
                    .FindAsync(p => productIds.Contains(p.Id))).ToList();

                if (products.Count != productIds.Count)
                {
                    return new OrderResultDto { Success = false, Message = "One or more products not found." };
                }

                decimal total = 0;
                var orderItems = new List<OrderItem>();

                // Process each ordered item
                foreach (var item in orderDto.Items)
                {
                    var product = products.First(p => p.Id == item.ProductId);

                    // Stock validation
                    if (product.StockQuantity < item.Quantity)
                    {
                        await _uow.RollbackTransactionAsync();
                        return new OrderResultDto
                        {
                            Success = false,
                            Message = $"Insufficient stock for product: {product.Name}"
                        };
                    }

                    // Reduce stock
                    product.StockQuantity -= item.Quantity;
                    await _uow.ProductRepository.UpdateAsync(product);

                    // Create order item
                    orderItems.Add(new OrderItem
                    {
                        ProductId = product.Id,
                        Quantity = item.Quantity,
                        UnitPrice = product.Price,
                        CreatedAt = DateTime.UtcNow
                    });

                    total += product.Price * item.Quantity;
                }

                // Create order entity
                var order = new Order
                {
                    OrderNumber = $"ORD-{Guid.NewGuid().ToString("N")[..10].ToUpper()}",
                    OrderDate = DateTime.UtcNow,
                    CustomerEmail = orderDto.CustomerEmail,
                    CustomerName = orderDto.CustomerName,
                    TotalAmount = total,
                    Status = OrderStatus.Pending,
                    OrderItems = orderItems,
                    CreatedAt = DateTime.UtcNow
                };

                await _uow.OrderRepository.AddAsync(order);

                await _uow.CommitTransactionAsync();

                return new OrderResultDto
                {
                    Success = true,
                    OrderId = order.Id,
                    OrderNumber = order.OrderNumber,
                    TotalAmount = order.TotalAmount,
                    Message = "Order placed successfully"
                };
            }
            catch
            {
                await _uow.RollbackTransactionAsync();
                return new OrderResultDto { Success = false, Message = "Error placing order." };
            }
        }

        // ---------------------------------------------
        // GET ORDER BY ID WITH ITEMS
        // ---------------------------------------------
        public async Task<OrderDto?> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetOrderWithItemsAsync(id);

            if (order == null)
                return null;

            return new OrderDto
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status.ToString(),
                CustomerEmail = order.CustomerEmail,
                CustomerName = order.CustomerName,
                Items = order.OrderItems.Select(oi => new OrderItemDetailDto
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product?.Name ?? "Unknown",
                    Sku = oi.Product?.Sku ?? "N/A",
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                    TotalPrice = oi.UnitPrice * oi.Quantity
                }).ToList()
            };
        }

        // ---------------------------------------------
        // GET ALL ORDERS
        // ---------------------------------------------
        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _uow.OrderRepository.GetAllAsync();

            return orders.Select(o => new OrderDto
            {
                Id = o.Id,
                OrderNumber = o.OrderNumber,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,
                Status = o.Status.ToString(),
                CustomerEmail = o.CustomerEmail,
                CustomerName = o.CustomerName
            });
        }
    }
}
