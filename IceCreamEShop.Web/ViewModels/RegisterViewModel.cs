using System.ComponentModel.DataAnnotations;

namespace IceCreamEShop.Web.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "請輸入電子郵件")]
        [EmailAddress(ErrorMessage = "電子郵件格式不正確")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "請輸入密碼")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "密碼長度必須在 6 到 100 個字元之間")]
        public string Passwd { get; set; } = null!;

        [Required(ErrorMessage = "請輸入使用者名稱")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "使用者名稱長度必須在 3 到 20 個字元之間")]
        public string UserName { get; set; } = null!;

        [Phone(ErrorMessage = "電話號碼格式不正確")]
        [RegularExpression(@"^09\d{8}$", ErrorMessage = "請輸入正確的手機號碼格式（如：0912345678）")]
        public string? PhoneNumber { get; set; }
    }
}
