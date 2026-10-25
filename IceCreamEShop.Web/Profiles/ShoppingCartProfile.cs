using AutoMapper;
using IceCreamEShop.Core.DTOs.ShoppingCart;
using IceCreamEShop.Web.ViewModels.ShoppingCart;

namespace IceCreamEShop.Web.Profiles
{
    public class ShoppingCartProfile : Profile
    {
        public ShoppingCartProfile()
        {
            CreateMap<GetCartDto, GetCartViewModel>();
        }
    }
}
