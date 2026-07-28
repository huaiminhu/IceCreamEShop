using System.ComponentModel.DataAnnotations;

namespace IceCreamEShop.Web.ViewModels.UserAccount
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "請輸入電子郵件")]
        [EmailAddress(ErrorMessage = "電子郵件格式不正確")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "請輸入密碼")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "密碼長度必須在 6 到 100 個字元之間")]
        public string Passwd { get; set; } = null!;
    }
}
