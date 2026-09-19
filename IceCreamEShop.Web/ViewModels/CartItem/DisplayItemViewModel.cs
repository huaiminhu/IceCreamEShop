namespace IceCreamEShop.Web.ViewModels.CartItem
{
    public class DisplayItemViewModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; } = null!;
        public int ProductPrice { get; set; }
        public string ProductPicUrl { get; set; } = null!;
    }
}
