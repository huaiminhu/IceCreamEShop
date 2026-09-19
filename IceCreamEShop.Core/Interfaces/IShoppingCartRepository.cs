
using IceCreamEShop.Core.Entities;

namespace IceCreamEShop.Core.Interfaces
{
    public interface IShoppingCartRepository
    {
        void Create(ShoppingCart cart);
        Task<ShoppingCart?> GetByAccountIdAsync(int userAccountId);  
    }
}
