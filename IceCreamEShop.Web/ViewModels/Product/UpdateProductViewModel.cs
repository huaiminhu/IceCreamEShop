using System.ComponentModel.DataAnnotations;

namespace IceCreamEShop.Web.ViewModels.Product
{
    public class UpdateProductViewModel
    {
        [Required]
        public int ProductId { get; set; }

        [StringLength(100, ErrorMessage = "商品名稱長度不能超過 {1} 個字")]
        [Display(Name = "商品名稱")]
        public string? ProductName { get; set; }

        [StringLength(50, ErrorMessage = "分類名稱長度不能超過 {1} 個字")]
        [Display(Name = "商品分類")]
        public string? Category { get; set; }

        [Required(ErrorMessage = "請提供商品描述！")]
        [StringLength(2000, ErrorMessage = "商品描述長度不能超過 {1} 個字")]
        [Display(Name = "商品描述")]
        public string ProductDescription { get; set; } = null!;

        [Range(0, 9999999, ErrorMessage = "價格必須在 {1} 到 {2} 之間")]
        [Display(Name = "商品價格")]
        public int? ProductPrice { get; set; }

        [Display(Name = "商品圖片檔案")]
        public IFormFile? ProductPic { get; set; }

        [Range(0, 99999, ErrorMessage = "數量不能為負數，且必須在 {1} 到 {2} 之間")]
        [Display(Name = "庫存數量")]
        public int? Quantity { get; set; }

        [Display(Name = "是否上架")]
        public bool? IsPublished { get; set; }
    }
}
