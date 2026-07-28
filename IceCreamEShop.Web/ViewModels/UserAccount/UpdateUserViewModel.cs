using System.ComponentModel.DataAnnotations;

namespace IceCreamEShop.Web.ViewModels.UserAccount
{
    public class UpdateUserViewModel
    {
        [Required(ErrorMessage = "請輸入使用者名稱")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "使用者名稱長度必須在 3 到 20 個字元之間")]
        public string UserName { get; set; } = null!;

        [Phone(ErrorMessage = "電話號碼格式不正確")]
        [RegularExpression(@"^09\d{8}$", ErrorMessage = "請輸入正確的手機號碼格式（如：0912345678）")]
        public string? PhoneNumber { get; set; }
    }
}
