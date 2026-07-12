using AutoMapper;
using IceCreamEShop.Core.DTOs.UserAccount;
using IceCreamEShop.Core.Enums;
using IceCreamEShop.Core.Interfaces;
using IceCreamEShop.Service.Services.IServices;

namespace IceCreamEShop.Service.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserAccountService(IUserAccountRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<LoginResponseDto?> AuthenticateAsync(LoginRequestDto request)
        {
            // 1. 取得使用者資訊, 以驗證是否有該使用者
            var user = await _unitOfWork.UserAccounts.GetByEmailAsync(request.Email);
            if (user == null) 
            {
                return null;
            }

            // 2. 驗證密碼
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Passwd, user.EnPassword);
            if (!isPasswordValid)
            {
                return null;
            }

            // 3. 轉成 DTO 回傳
            var response = _mapper.Map<LoginResponseDto>(user);
            return response;
        }

        public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            // 1. 檢查 Email 是否已經註冊過
            bool isUnique = await _unitOfWork.UserAccounts.IsEmailUniqueAsync(request.Email);
            if (!isUnique)
            {
                return new RegisterResponseDto { Message = "該 Email 已被註冊" };
            }

            // 2. 密碼雜湊處理
            var hashedPasswd = BCrypt.Net.BCrypt.HashPassword(request.Passwd);

            // 3. 新增使用者
            var userId = await _unitOfWork.UserAccounts.AddAsync(hashedPasswd, request, UserRole.General_User);
            await _unitOfWork.CompleteAsync();

            return new RegisterResponseDto { UserAccountId = userId, IsSuccess = true, Message = "註冊成功" };
        }

        public async Task<RegisterResponseDto> CreateUserByAdminAsync(RegisterRequestDto request, UserRole currentRole, UserRole targetRole)
        {
            // 防止店家管理員自己把自己升級成系統管理員
            if (targetRole == UserRole.Sys_Manager && currentRole == UserRole.EShop_Manager)
            {
                return new RegisterResponseDto { Message = "註冊失敗" };
            }

            bool isUnique = await _unitOfWork.UserAccounts.IsEmailUniqueAsync(request.Email);
            if (!isUnique)
            {
                return new RegisterResponseDto { Message = "該 email 已被註冊" };
            } 

            var hashedPasswd = BCrypt.Net.BCrypt.HashPassword(request.Passwd);

            // 根據後台管理員的選擇，動態傳入對應的角色
            var newUserId = await _unitOfWork.UserAccounts.AddAsync(hashedPasswd, request, targetRole);
            await _unitOfWork.CompleteAsync();

            return new RegisterResponseDto { UserAccountId = newUserId, IsSuccess = true, Message = "後台帳號建立成功" };
        }

    }

}
