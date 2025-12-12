using System;
using System.Threading;
using System.Threading.Tasks;
using ProductCatalog.Core.Entities;
using ProductCatalog.DataAccess.Repositories.Interface;


namespace ProductCatalog.Core.Contracts.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product> ProductRepository { get; }
        IOrderRepository OrderRepository { get; }           
        IRepository<OrderItem> OrderItemRepository { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
