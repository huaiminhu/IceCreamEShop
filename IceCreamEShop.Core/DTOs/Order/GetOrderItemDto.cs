using IceCreamEShop.Core.DTOs.Product;

namespace IceCreamEShop.Core.DTOs.Order
{
    public class GetOrderItemDto
    {
        public int ItemQuantity { get; set; }
        public ProductDto OrderItem { get; set; } = null!;
    }
}
