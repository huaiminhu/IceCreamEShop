
using IceCreamEShop.Core.DTOs.CartItem;
using IceCreamEShop.Core.DTOs.Product;

namespace IceCreamEShop.Service.Services.IServices
{
    public interface IShoppingCartService
    {
        Task<int> AddItemToCartAsync(int userAccountId, CreateItemDto dto); 
        Task<List<ProductDto>?> GetCartItemsAsync(int userAccountId);
        Task<int> PutItemsAwayFromCartAsync(int userAccountId, List<RemoveItemDto> dto);
    }
}
