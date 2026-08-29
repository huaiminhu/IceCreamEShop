using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCreamEShop.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IUserAccountRepository UserAccounts { get; }
        IProductRepository Products { get; }

        Task<int> CompleteAsync();
    }
}
