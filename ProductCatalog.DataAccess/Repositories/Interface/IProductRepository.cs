using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCatalog.Core.Entities;

namespace ProductCatalog.DataAccess.Repositories.Interface
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product?> GetBySkuAsync(string sku);
        Task<bool> SkuExistsAsync(string sku, int? excludeId = null);
    }
}
