using AutoMapper;
using IceCreamEShop.Core.DTOs.ShoppingCart;
using IceCreamEShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCreamEShop.Service.Profiles
{
    public class ShoppingCartProfile : Profile
    {
        public ShoppingCartProfile()
        {
            CreateMap<ShoppingCart, GetCartDto>();
        }
    }
}
