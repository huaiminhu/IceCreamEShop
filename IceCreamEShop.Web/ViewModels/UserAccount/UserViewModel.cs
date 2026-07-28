namespace IceCreamEShop.Web.ViewModels.UserAccount
{
    public class UserViewModel
    {
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? CurrentPasswd { get; set; }
        public string? NewPasswd { get; set; }
        public string? ComfirmPasswd { get; set; }
    }
}
