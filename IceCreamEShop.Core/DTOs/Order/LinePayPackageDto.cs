using IceCreamEShop.Core.DTOs.Product;

namespace IceCreamEShop.Core.DTOs.Order
{
    public class LinePayPackageDto
    {
        public string Id { get; set; } = null!;
        public int Amount { get; set; }
        public List<ProductDto> Products { get; set; } = null!;
    }
}
