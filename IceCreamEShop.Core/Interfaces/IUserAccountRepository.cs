using IceCreamEShop.Core.DTOs.UserAccount;
using IceCreamEShop.Core.Entities;
using IceCreamEShop.Core.Enums;

namespace IceCreamEShop.Core.Interfaces
{
    public interface IUserAccountRepository
    {
        Task<UserAccount?> GetByIdAsync(int id);
        Task<UserAccount?> GetByEmailAsync(string email);
        Task<bool> IsEmailUniqueAsync(string email);

        void Create(UserAccount user);
        void Update(UserAccount user);
        void Delete(UserAccount user);
    }
}
