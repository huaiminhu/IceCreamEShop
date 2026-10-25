using IceCreamEShop.Core.Entities;
using IceCreamEShop.Core.Interfaces;
using IceCreamEShop.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace IceCreamEShop.Repository.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly IceCreamEShopContext _context;
        public OrderItemRepository(IceCreamEShopContext context)
        {
            _context = context;
        }
        public void Create(OrderItem oitem)
        {
            _context.OrderItems.Add(oitem);
        }

        public async Task<List<OrderItem>?> GetItemsByOrderIdAsync(int orderId)
        {
            return await _context.OrderItems.AsNoTracking().Where(i => i.OrderInfoId == orderId).ToListAsync();
        }

        public void Delete(int orderId) 
        {
            var ditems = _context.OrderItems.AsNoTracking().Where(i => i.OrderInfoId == orderId);
            if (ditems.Any()) 
            {
                _context.OrderItems.RemoveRange(ditems);
            }
        }
    }
}
