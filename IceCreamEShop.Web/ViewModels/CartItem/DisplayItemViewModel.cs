using IceCreamEShop.Web.ViewModels.Product;

namespace IceCreamEShop.Web.ViewModels.CartItem
{
    public class DisplayItemViewModel
    {
        public DisplayProductViewModel DisplayedItem { get; set; } = null!;
        public int ItemQuantity { get; set; }
        public int TotalPrice { get; set; }
    }
}
