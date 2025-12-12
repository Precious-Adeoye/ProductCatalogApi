using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductCatalog.Core.Entities;
using ProductCatalog.Core.Enums;
using ProductCatalog.Data.Context;

namespace ProductCatalog.Data.Seeds
{
    public static class SeedData
    {
        public static async Task InitializeAsync(ApplicationDbContext context, ILogger logger)
        {
            try
            {
                // Apply pending migrations
                await context.Database.MigrateAsync();

                // Check if data already exists
                if (await context.Products.AnyAsync())
                {
                    logger.LogInformation("Database already seeded");
                    return;
                }

                logger.LogInformation("Seeding database...");

                // Seed Products
                var products = new List<Product>
                {
                    new Product
                    {
                        Name = "Laptop Pro X1",
                        Description = "High-performance laptop with 16GB RAM and 1TB SSD",
                        Price = 1299.99m,
                        StockQuantity = 50,
                        Sku = "LPX1-2024",
                        CreatedAt = DateTime.UtcNow
                    },
                    new Product
                    {
                        Name = "Wireless Mouse",
                        Description = "Ergonomic wireless mouse with long battery life",
                        Price = 29.99m,
                        StockQuantity = 200,
                        Sku = "WM-ERG01",
                        CreatedAt = DateTime.UtcNow
                    },
                    new Product
                    {
                        Name = "Mechanical Keyboard",
                        Description = "RGB mechanical keyboard with cherry MX switches",
                        Price = 89.99m,
                        StockQuantity = 75,
                        Sku = "MK-RGB01",
                        CreatedAt = DateTime.UtcNow
                    },
                    new Product
                    {
                        Name = "4K Monitor",
                        Description = "27-inch 4K UHD monitor with HDR support",
                        Price = 399.99m,
                        StockQuantity = 30,
                        Sku = "MON-4K27",
                        CreatedAt = DateTime.UtcNow
                    },
                    new Product
                    {
                        Name = "USB-C Hub",
                        Description = "7-in-1 USB-C hub with HDMI, Ethernet, and USB ports",
                        Price = 49.99m,
                        StockQuantity = 150,
                        Sku = "HUB-7IN1",
                        CreatedAt = DateTime.UtcNow
                    }
                };

                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();

                logger.LogInformation("Seeded {Count} products", products.Count);

                // Seed Sample Orders
                var orders = new List<Order>
                {
                    new Order
                    {
                        OrderNumber = "ORD-20240115-0001",
                        OrderDate = DateTime.UtcNow.AddDays(-10),
                        CustomerEmail = "john.doe@example.com",
                        CustomerName = "John Doe",
                        TotalAmount = 1429.98m,
                        Status = OrderStatus.Completed,
                        CreatedAt = DateTime.UtcNow.AddDays(-10),
                        OrderItems = new List<OrderItem>
                        {
                            new OrderItem
                            {
                                ProductId = products[0].Id,
                                Quantity = 1,
                                UnitPrice = products[0].Price,
                                CreatedAt = DateTime.UtcNow.AddDays(-10)
                            },
                            new OrderItem
                            {
                                ProductId = products[3].Id,
                                Quantity = 1,
                                UnitPrice = products[3].Price,
                                CreatedAt = DateTime.UtcNow.AddDays(-10)
                            }
                        }
                    },
                    new Order
                    {
                        OrderNumber = "ORD-20240116-0002",
                        OrderDate = DateTime.UtcNow.AddDays(-5),
                        CustomerEmail = "jane.smith@example.com",
                        CustomerName = "Jane Smith",
                        TotalAmount = 169.97m,
                        Status = OrderStatus.Processing,
                        CreatedAt = DateTime.UtcNow.AddDays(-5),
                        OrderItems = new List<OrderItem>
                        {
                            new OrderItem
                            {
                                ProductId = products[1].Id,
                                Quantity = 2,
                                UnitPrice = products[1].Price,
                                CreatedAt = DateTime.UtcNow.AddDays(-5)
                            },
                            new OrderItem
                            {
                                ProductId = products[2].Id,
                                Quantity = 1,
                                UnitPrice = products[2].Price,
                                CreatedAt = DateTime.UtcNow.AddDays(-5)
                            }
                        }
                    }
                };

                await context.Orders.AddRangeAsync(orders);
                await context.SaveChangesAsync();

                logger.LogInformation("Seeded {Count} orders", orders.Count);
                logger.LogInformation("Database seeding completed successfully");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database");
                throw;
            }
        }
    }
}
