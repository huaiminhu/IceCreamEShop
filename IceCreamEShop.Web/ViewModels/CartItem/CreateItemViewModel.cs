using System.ComponentModel.DataAnnotations;

namespace IceCreamEShop.Web.ViewModels.CartItem
{
    public class CreateItemViewModel
    {
        [Required]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "未提供數量！")]
        [Range(0, 99999, ErrorMessage = "數量必須大於0！")]
        public int Quantity { get; set; }
    }
}
