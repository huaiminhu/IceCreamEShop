using AutoMapper;
using IceCreamEShop.Core.DTOs.UserAccount;
using IceCreamEShop.Web.ViewModels.UserAccount;

namespace IceCreamEShop.Web.Profiles
{
    public class UserAccountProfile : Profile
    {
        public UserAccountProfile()
        {
            CreateMap<LoginViewModel, LoginRequestDto>();
            CreateMap<RegisterViewModel, RegisterRequestDto>();
            CreateMap<UpdateUserViewModel, UpdateUserDto>();
            CreateMap<ChangePasswdViewModel, ChangePasswdDto>();
            CreateMap<UserDto, UserViewModel>();
        }
    }
}
