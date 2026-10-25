using IceCreamEShop.Core.DTOs.CartItem;
using IceCreamEShop.Core.DTOs.Product;

namespace IceCreamEShop.Core.DTOs.ShoppingCart
{
    public class GetCartDto
    {
        public int ShoppingCartId { get; set; }
        public int UserAccountId { get; set; }
        public List<DisplayItemDto>? CartItems { get; set; }
        public int TotalPrice { get; set; }
    }
}
