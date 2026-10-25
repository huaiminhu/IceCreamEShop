using AutoMapper;
using IceCreamEShop.Core.DTOs.Order;
using IceCreamEShop.Web.ViewModels.Order;

namespace IceCreamEShop.Web.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<CreateItemViewModel, CreateOrderItemDto>();
            CreateMap<CreateOrderViewModel, CreateOrderDto>();
            CreateMap<GetOrderDto, GetOrderViewModel>();
            CreateMap<GetOrderItemDto, GetOrderItemViewModel>();
            CreateMap<PaymentRequestViewModel, PaymentRequestDto>();
            CreateMap<LinePayConfirmViewModel, LinePayConfirmDto>();
            CreateMap<LinePayPackageViewModel, LinePayPackageDto>();
            CreateMap<UnpaidOrderDto, UnpaidOrderViewModel>();
            CreateMap<UserOrderDto, UserOrderViewModel>();
        }
    }
}
