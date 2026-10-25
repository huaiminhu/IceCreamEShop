using IceCreamEShop.Core.DTOs.Order;
using System.ComponentModel.DataAnnotations;

namespace IceCreamEShop.Web.ViewModels.Order
{
    public class PaymentRequestViewModel
    {
        [Required]
        public int Amount { get; set; }
        [Required]
        public int OrderId { get; set; }
        public List<LinePayPackageViewModel>? Packages { get; set; }
        [Required]
        public string Provider { get; set; } = null!;
    }
}
