using IceCreamEShop.Core.Entities;
using IceCreamEShop.Core.Common;

namespace IceCreamEShop.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id);
        Task<PageObject<Product>?> GetPageProductsAsync(int pageNumber, int pageSize);
        void Create(Product product);
        void Update(Product product);
        void Delete(Product product);
    }
}
