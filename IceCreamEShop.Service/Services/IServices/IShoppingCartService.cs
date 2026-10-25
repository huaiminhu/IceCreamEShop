
using IceCreamEShop.Core.DTOs.CartItem;
using IceCreamEShop.Core.DTOs.ShoppingCart;

namespace IceCreamEShop.Service.Services.IServices
{
    public interface IShoppingCartService
    {
        Task<int> AddItemToCartAsync(int userAccountId, CreateItemDto dto); 
        Task<GetCartDto?> GetCartAsync(int userAccountId);
        Task<int> PutItemsAwayFromCartAsync(int userAccountId, List<RemoveItemDto> dto);
    }
}
