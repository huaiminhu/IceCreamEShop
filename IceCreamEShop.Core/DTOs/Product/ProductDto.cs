
namespace IceCreamEShop.Core.DTOs.Product
{
    public class ProductDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string ProductDescription { get; set; } = null!;

        public int ProductPrice { get; set; }

        public string ProductPicUrl { get; set; } = null!;

        public int Quantity { get; set; }

        public bool IsPublished { get; set; }
    }
}
