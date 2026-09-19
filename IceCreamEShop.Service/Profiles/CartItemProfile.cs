using AutoMapper;
using IceCreamEShop.Core.DTOs.CartItem;
using IceCreamEShop.Core.Entities;

namespace IceCreamEShop.Service.Profiles
{
    public class CartItemProfile : Profile
    {
        public CartItemProfile()
        {
            CreateMap<CreateItemDto, CartItem>();
        }
    }
}
