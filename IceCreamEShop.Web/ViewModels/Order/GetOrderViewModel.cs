
namespace IceCreamEShop.Web.ViewModels.Order
{
    public class GetOrderViewModel
    {
        public int OrderInfoId { get; set; }
        public int PaymentAmount { get; set; }
        public string PaymentProvider { get; set; } = null!;
        public int PaymentStatus { get; set; }
        public List<GetOrderItemViewModel> OrderItems { get; set; } = null!;
    }
}
