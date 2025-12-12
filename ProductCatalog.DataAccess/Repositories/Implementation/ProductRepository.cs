using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Core.Entities;
using ProductCatalog.Data.Context;
using ProductCatalog.DataAccess.Repositories.Interface;

namespace ProductCatalog.DataAccess.Repositories.Implementation
{
   

    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Product?> GetBySkuAsync(string sku) => await _dbSet.FirstOrDefaultAsync(p => p.Sku == sku);
        public async Task<bool> SkuExistsAsync(string sku, int? excludeId = null)
        {
            var q = _dbSet.AsQueryable().Where(p => p.Sku == sku);
            if (excludeId.HasValue) q = q.Where(p => p.Id != excludeId.Value);
            return await q.AnyAsync();
        }
    }
}
