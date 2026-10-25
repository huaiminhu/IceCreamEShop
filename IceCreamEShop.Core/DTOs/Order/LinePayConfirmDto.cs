
namespace IceCreamEShop.Core.DTOs.Order
{
    public class LinePayConfirmDto
    {
        public string TransactionId { get; set; } = null!;
        public int OrderId { get; set; }
    }
}
