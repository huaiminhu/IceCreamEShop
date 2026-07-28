using IceCreamEShop.Core.DTOs.UserAccount;
using IceCreamEShop.Core.Entities;
using IceCreamEShop.Core.Enums;
using IceCreamEShop.Core.Interfaces;
using IceCreamEShop.Repository.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;

namespace IceCreamEShop.Repository.Repositories
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly IceCreamEShopContext _context; 

        public UserAccountRepository(IceCreamEShopContext context)
        {
            _context = context;
        }

        public async Task<UserAccount?> GetByIdAsync(int id)
        {
            return await _context.UserAccounts
                .FirstOrDefaultAsync(u => u.UserAccountId == id);
        }

        public async Task<UserAccount?> GetByEmailAsync(string email)
        {
            return await _context.UserAccounts
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            bool exists = await _context.UserAccounts
                .AnyAsync(u => u.Email == email);

            return !exists;
        }

        public void Create(UserAccount user)
        {
            _context.UserAccounts.Add(user);
        }

        public void Update(UserAccount user)
        {
            _context.UserAccounts.Update(user);
        }

        public void Delete(UserAccount user)
        {
            _context.UserAccounts.Remove(user);
        }
    }
}
