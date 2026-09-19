using AutoMapper;
using IceCreamEShop.Core.DTOs.CartItem;
using IceCreamEShop.Web.ViewModels.CartItem;

namespace IceCreamEShop.Web.Profiles
{
    public class CartItemProfile : Profile
    {
        public CartItemProfile()
        {
            CreateMap<CreateItemViewModel, CreateItemDto>();
            CreateMap<DisplayItemDto, DisplayItemViewModel>();
            CreateMap<RemoveItemViewModel, RemoveItemDto>();
        }
    }
}
