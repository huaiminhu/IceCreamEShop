using IceCreamEShop.Web.ViewModels.Product;

namespace IceCreamEShop.Web.ViewModels.Order
{
    public class GetOrderItemViewModel
    {
        public int ItemQuantity { get; set; }
        public DisplayProductViewModel OrderItem { get; set; } = null!;
    }
}
