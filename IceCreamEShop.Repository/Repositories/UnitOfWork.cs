using IceCreamEShop.Core.Interfaces;
using IceCreamEShop.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCreamEShop.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IceCreamEShopContext _context;
        public UnitOfWork(IceCreamEShopContext context)
        {
            _context = context;
            UserAccounts = new UserAccountRepository(_context);
            Products = new ProductRepository(_context);
        }

        public IUserAccountRepository UserAccounts { get; private set; }
        public IProductRepository Products { get; set; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
