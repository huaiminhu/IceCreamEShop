using System.ComponentModel.DataAnnotations;

namespace IceCreamEShop.Web.ViewModels.Order
{
    public class CreateOrderViewModel
    {
        [Required]
        public int UserAccountId { get; set; }
        [Required]
        public int PaymentAmount { get; set; }
        [Required]
        public List<CreateItemViewModel> OrderItems { get; set; } = null!;
    }
}
