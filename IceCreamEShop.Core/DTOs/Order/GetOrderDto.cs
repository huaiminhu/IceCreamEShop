
namespace IceCreamEShop.Core.DTOs.Order
{
    public class GetOrderDto
    {
        public int OrderInfoId { get; set; }
        public int PaymentAmount { get; set; }
        public string PaymentProvider { get; set; } = null!;
        public int PaymentStatus { get; set; }
        public List<GetOrderItemDto> OrderItems { get; set; } = null!;
    }
}
