using IceCreamEShop.Core.DTOs.Order;
using IceCreamEShop.Web.ViewModels.Product;
using System.ComponentModel.DataAnnotations;

namespace IceCreamEShop.Web.ViewModels.Order
{
    public class LinePayPackageViewModel
    {
        [Required]
        public string Id { get; set; } = null!;
        [Required]
        public int Amount { get; set; }
        [Required]
        public List<LinePayPackageItemsViewModel> Products { get; set; } = null!;
    }
}
