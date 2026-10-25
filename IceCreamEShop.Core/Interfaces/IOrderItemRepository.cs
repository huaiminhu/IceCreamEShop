using IceCreamEShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCreamEShop.Core.Interfaces
{
    public interface IOrderItemRepository
    {
        void Create(OrderItem oitem);
        Task<List<OrderItem>?> GetItemsByOrderIdAsync(int orderId);
        void Delete(int orderId);
    }
}
