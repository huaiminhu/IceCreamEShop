namespace IceCreamEShop.Web.ViewModels.Order
{
    public class UnpaidOrderViewModel
    {
        public int OrderInfoId { get; set; }
        public int PaymentAmount { get; set; }
        public string PaymentProvider { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}
