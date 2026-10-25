
namespace IceCreamEShop.Core.DTOs.Order
{
    public class PaymentRequestDto
    {
        public int Amount { get; set; }
        public string Currency { get; set; } = "TWD";
        public int OrderId { get; set; }
        public List<LinePayPackageDto>? Packages { get; set; }
        public LinePayRedirectUrlsDto RedirectUrls { get; set; } = new LinePayRedirectUrlsDto();
    }
}
