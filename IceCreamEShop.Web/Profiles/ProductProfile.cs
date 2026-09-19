using AutoMapper;
using IceCreamEShop.Core.DTOs.Product;
using IceCreamEShop.Web.ViewModels.Product;

namespace IceCreamEShop.Web.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductViewModel, ProductDto>();
            CreateMap<UpdateProductViewModel, UpdateProductDto>();
            CreateMap<ProductDto, ProductViewModel>();
            CreateMap<ProductDto, DisplayProductViewModel>();
        }
    }
}
