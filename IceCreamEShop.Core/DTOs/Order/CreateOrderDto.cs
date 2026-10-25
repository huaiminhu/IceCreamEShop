
namespace IceCreamEShop.Core.DTOs.Order
{
    public class CreateOrderDto
    {
        public int UserAccountId { get; set; }
        public int PaymentAmount { get; set; }
        public List<CreateOrderItemDto> OrderItems { get; set; } = null!;
    }
}
