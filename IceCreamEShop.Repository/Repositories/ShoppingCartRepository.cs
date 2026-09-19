
using IceCreamEShop.Core.Entities;
using IceCreamEShop.Core.Interfaces;
using IceCreamEShop.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace IceCreamEShop.Repository.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly IceCreamEShopContext _context;
        public ShoppingCartRepository(IceCreamEShopContext context)
        {
            _context = context;
        }

        public void Create(ShoppingCart cart)
        {
            _context.ShoppingCarts.Add(cart);
        }

        public async Task<ShoppingCart?> GetByAccountIdAsync(int userAccountId)
        {
            return await _context.ShoppingCarts.FirstOrDefaultAsync(c => c.UserAccountId == userAccountId);
        }
    }
}
