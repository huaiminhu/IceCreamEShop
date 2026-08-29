using IceCreamEShop.Core.Common;
using IceCreamEShop.Core.DTOs.Product;

namespace IceCreamEShop.Service.Services.IServices
{
    public interface IProductService
    {
        Task<ProductDto?> GetProductAsync(int id);
        Task<PageObject<ProductDto>?> GetPageProductsAsync(int pageNumber, int pageSize);
        Task<int> CreateProductAsync(ProductDto dto);
        Task<int> UpdateProductAsync(UpdateProductDto dto);
        Task<int> DeleteProductAsync(int productId);
    }
}
