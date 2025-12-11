using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Core.Exceptions
{
    public abstract class DomainException : Exception
    {
        protected DomainException(string message) : base(message)
        {
        }

        protected DomainException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public class InsufficientStockException : DomainException
    {
        public string ProductName { get; }
        public int AvailableStock { get; }
        public int RequestedQuantity { get; }

        public InsufficientStockException(string productName, int availableStock, int requestedQuantity)
            : base($"Insufficient stock for product '{productName}'. Available: {availableStock}, Requested: {requestedQuantity}")
        {
            ProductName = productName;
            AvailableStock = availableStock;
            RequestedQuantity = requestedQuantity;
        }
    }

    public class ProductNotFoundException : DomainException
    {
        public int ProductId { get; }

        public ProductNotFoundException(int productId)
            : base($"Product with ID {productId} was not found")
        {
            ProductId = productId;
        }
    }

    public class OrderNotFoundException : DomainException
    {
        public int OrderId { get; }

        public OrderNotFoundException(int orderId)
            : base($"Order with ID {orderId} was not found")
        {
            OrderId = orderId;
        }
    }
}
