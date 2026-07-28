using System.ComponentModel.DataAnnotations;

namespace IceCreamEShop.Web.ViewModels.UserAccount
{
    public class ChangePasswdViewModel
    {
        [Required(ErrorMessage = "請輸入密碼")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "密碼長度必須在 6 到 100 個字元之間")]
        public string CurrentPasswd { get; set; } = null!;
        [Required(ErrorMessage = "請輸入密碼")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "密碼長度必須在 6 到 100 個字元之間")]
        public string NewPasswd { get; set; } = null!;
        [Required(ErrorMessage = "請再次確認密碼")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "密碼長度必須在 6 到 100 個字元之間")]
        public string ComfirmPasswd { get; set; } = null!;
    }
}
