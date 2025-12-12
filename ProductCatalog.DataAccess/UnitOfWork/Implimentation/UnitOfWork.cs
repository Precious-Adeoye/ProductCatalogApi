using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using ProductCatalog.Core.Contracts.Interface;
using ProductCatalog.Core.Entities;
using ProductCatalog.Data.Context;
using ProductCatalog.DataAccess.Repositories.Implementation;
using ProductCatalog.DataAccess.Repositories.Interface;


namespace ProductCatalog.DataAccess.UnitOfWork.Implimentation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _tx;

        public IRepository<Product> ProductRepository { get; }
        public IOrderRepository OrderRepository { get; }   // FIXED
        public IRepository<OrderItem> OrderItemRepository { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            ProductRepository = new ProductRepository(context);
            OrderRepository = new OrderRepository(context);  // FIXED
            OrderItemRepository = new Repository<OrderItem>(context);
        }

        public async Task BeginTransactionAsync() =>
            _tx = await _context.Database.BeginTransactionAsync();

        public async Task CommitTransactionAsync()
        {
            await _context.SaveChangesAsync();
            if (_tx != null) await _tx.CommitAsync();
            _tx?.Dispose();
            _tx = null;
        }

        public async Task RollbackTransactionAsync()
        {
            if (_tx != null) await _tx.RollbackAsync();
            _tx?.Dispose();
            _tx = null;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            await _context.SaveChangesAsync(cancellationToken);

        public void Dispose()
        {
            _context?.Dispose();
            _tx?.Dispose();
        }
    }
}
