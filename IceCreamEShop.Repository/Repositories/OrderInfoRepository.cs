using IceCreamEShop.Core.Entities;
using IceCreamEShop.Core.Interfaces;
using IceCreamEShop.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace IceCreamEShop.Repository.Repositories
{
    public class OrderInfoRepository : IOrderInfoRepository
    {
        private readonly IceCreamEShopContext _context;
        public OrderInfoRepository(IceCreamEShopContext context)
        {
            _context = context;
        }
        public void Create(OrderInfo order)
        {
            _context.OrderInfos.Add(order);
        }

        public void Delete(OrderInfo order)
        {
            _context.OrderInfos.Remove(order);
        }

        public async Task<OrderInfo?> GetByOrderIdAsync(int orderId)
        {
            return await _context.OrderInfos.AsNoTracking()
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.OrderInfoId == orderId);
        }

        public void Update(OrderInfo order)
        {
            _context.OrderInfos.Update(order);
        }

        public async Task<OrderInfo?> GetByPhoneNumberAsync(string phoneNumber)
        {
            return await _context.OrderInfos.AsNoTracking()
                .SingleOrDefaultAsync(o => o.PaymentStatus == 0 && o.UserAccount.PhoneNumber == phoneNumber);
        }

        public async Task<List<OrderInfo>> GetByUserIdAsync(int userId)
        {
            return await _context.OrderInfos.AsNoTracking()
                .Where(o => o.UserAccountId == userId)
                .ToListAsync();
        }
    }
}
