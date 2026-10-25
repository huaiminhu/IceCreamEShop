using IceCreamEShop.Web.ViewModels.CartItem;

namespace IceCreamEShop.Web.ViewModels.ShoppingCart
{
    public class GetCartViewModel
    {
        public int ShoppingCartId { get; set; }
        public int UserAccountId { get; set; }
        public List<DisplayItemViewModel>? CartItems { get; set; }
        public int TotalPrice { get; set; }
    }
}
