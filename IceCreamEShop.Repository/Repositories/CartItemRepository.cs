using IceCreamEShop.Core.Entities;
using IceCreamEShop.Core.Interfaces;
using IceCreamEShop.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace IceCreamEShop.Repository.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly IceCreamEShopContext _context;
        public CartItemRepository(IceCreamEShopContext context)
        {
            _context = context;
        }
        public void Create(CartItem cartItem)
        {
            _context.CartItems.Add(cartItem);
        }

        public void Delete(List<CartItem> cartItems)
        {
            _context.CartItems.RemoveRange(cartItems);
        }

        public async Task<List<CartItem>?> GetItemsByCartAsync(int shoppingCartId)
        {
            return await _context.CartItems.Where(cis => cis.ShoppingCartId == shoppingCartId).ToListAsync();
        }

        public async Task<CartItem?> GetItemAsync(int shoppingCartId, int productId)
        {
            return await _context.CartItems.SingleOrDefaultAsync(c => c.ShoppingCartId == shoppingCartId && c.ProductId == productId);
        }
    }
}
