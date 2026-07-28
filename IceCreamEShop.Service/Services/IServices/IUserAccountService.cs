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

        // 讀取使用者資訊
        Task<UserDto?> GetUserAsync(int id);

        // 更新使用者資訊
        Task<int> UpdateUserAsync(UpdateUserDto userDto);

        // 更換密碼
        Task<int> ChangePasswordAsync(ChangePasswdDto passwdDto);

        // 移除使用者資訊
        Task<int> DeleteUserAsync(int id);
    }

}
