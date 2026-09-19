using IceCreamEShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCreamEShop.Core.Interfaces
{
    public interface ICartItemRepository
    {
        Task<List<CartItem>?> GetItemsByCartAsync(int shoppingCartId);
        void Create(CartItem cartItem);
        void Delete(List<CartItem> cartItems);
        Task<CartItem?> GetItemAsync(int shoppingCartId, int productId);
    }
}
