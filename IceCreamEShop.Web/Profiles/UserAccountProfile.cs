using AutoMapper;
using IceCreamEShop.Core.DTOs.UserAccount;
using IceCreamEShop.Web.ViewModels;

namespace IceCreamEShop.Web.Profiles
{
    public class UserAccountProfile : Profile
    {
        public UserAccountProfile()
        {
            CreateMap<LoginViewModel, LoginRequestDto>();
            CreateMap<RegisterViewModel, RegisterRequestDto>();
        }
    }
}
