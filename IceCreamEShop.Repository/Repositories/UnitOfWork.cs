using IceCreamEShop.Core.Entities;
using IceCreamEShop.Core.Interfaces;
using IceCreamEShop.Repository.Data;

namespace IceCreamEShop.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IceCreamEShopContext _context;
        public UnitOfWork(IceCreamEShopContext context)
        {
            _context = context;
            UserAccounts = new UserAccountRepository(_context);
            Products = new ProductRepository(_context);
            ShoppingCarts = new ShoppingCartRepository(_context);
            CartItems = new CartItemRepository(_context);
        }

        public IUserAccountRepository UserAccounts { get; private set; }
        public IProductRepository Products { get; set; }
        public IShoppingCartRepository ShoppingCarts { get; set; }
        public ICartItemRepository CartItems { get; set; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
