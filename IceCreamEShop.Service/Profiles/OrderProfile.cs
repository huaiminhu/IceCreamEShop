using AutoMapper;
using IceCreamEShop.Core.DTOs.Order;
using IceCreamEShop.Core.Entities;

namespace IceCreamEShop.Service.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<CreateOrderItemDto, OrderItem>();
            CreateMap<CreateOrderDto, OrderInfo>();
            CreateMap<OrderInfo, GetOrderDto>();
            CreateMap<OrderItem, GetOrderItemDto>();
            CreateMap<OrderInfo, UnpaidOrderDto>();
            CreateMap<OrderInfo, UserOrderDto>();
        }
    }
}
