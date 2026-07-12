using IceCreamEShop.Core.DTOs.UserAccount;
using IceCreamEShop.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCreamEShop.Service.Services.IServices
{
    public interface IUserAccountService
    {
        // 登入驗證
        Task<LoginResponseDto?> AuthenticateAsync(LoginRequestDto request);

        // 註冊新帳號(一般使用者)
        Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request);

        // 新增後臺管理/系統管理員
        Task<RegisterResponseDto> CreateUserByAdminAsync(RegisterRequestDto request, UserRole currentRole, UserRole targetRole);
    }

}
