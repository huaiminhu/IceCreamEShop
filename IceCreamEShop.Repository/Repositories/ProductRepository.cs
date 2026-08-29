using IceCreamEShop.Core.Common;
using IceCreamEShop.Core.Entities;
using IceCreamEShop.Core.Interfaces;
using IceCreamEShop.Repository.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace IceCreamEShop.Repository.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IceCreamEShopContext _context;
        public ProductRepository(IceCreamEShopContext context)
        {
            _context = context;
        }
        public void Create(Product product)
        {
            _context.Products.Add(product);
        }

        public void Delete(Product product)
        {
            _context.Products.Remove(product);
        }

        public async Task<PageObject<Product>?> GetPageProductsAsync(int pageNumber, int pageSize)
        {
            var query = _context.Products.AsNoTracking();

            int totalCount = await query.CountAsync();

            // 分頁計算並取得目前頁面的資料
            var items = await query
                .Skip((pageNumber - 1) * pageSize) // 跳過前面頁數的資料
                .Take(pageSize)                   // 只取出目前頁面需要的筆數
                .ToListAsync();

            return new PageObject<Product>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }
    }
}
