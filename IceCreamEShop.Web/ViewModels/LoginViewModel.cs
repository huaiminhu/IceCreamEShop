using System.ComponentModel.DataAnnotations;

namespace IceCreamEShop.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "請輸入電子郵件")]
        [EmailAddress(ErrorMessage = "電子郵件格式不正確")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "請輸入密碼")]
        [DataType(DataType.Password)]
        public string Passwd { get; set; } = null!;
    }
}
