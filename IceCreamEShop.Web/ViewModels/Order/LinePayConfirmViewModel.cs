using System.ComponentModel.DataAnnotations;

namespace IceCreamEShop.Web.ViewModels.Order
{
    public class LinePayConfirmViewModel
    {
        [Required]
        public string TransactionId { get; set; } = null!;
        [Required]
        public int OrderId { get; set; }
    }
}
