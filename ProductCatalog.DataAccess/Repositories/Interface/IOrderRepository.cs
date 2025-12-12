using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCatalog.Core.Entities;

namespace ProductCatalog.DataAccess.Repositories.Interface
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> GetOrderWithItemsAsync(int id);

    }

}
