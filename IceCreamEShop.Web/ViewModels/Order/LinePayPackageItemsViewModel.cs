using System.ComponentModel.DataAnnotations;

namespace IceCreamEShop.Web.ViewModels.Order
{
    public class LinePayPackageItemsViewModel
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public int Price { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
