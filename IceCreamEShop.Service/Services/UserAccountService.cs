using AutoMapper;
using IceCreamEShop.Core.DTOs.UserAccount;
using IceCreamEShop.Core.Entities;
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
            // 檢查 Email 是否已經註冊過
            bool isUnique = await _unitOfWork.UserAccounts.IsEmailUniqueAsync(request.Email);
            if (!isUnique)
            {
                return new RegisterResponseDto { Message = "該 Email 已被註冊" };
            }

            // 新增使用者
            var user = _mapper.Map<UserAccount>(request);
            // 密碼雜湊處理
            user.EnPassword = BCrypt.Net.BCrypt.HashPassword(request.Passwd);
            _unitOfWork.UserAccounts.Create(user);
            var result = await _unitOfWork.CompleteAsync();
            if (result < 1)
            {
                return new RegisterResponseDto { IsSuccess = false, Message = "註冊失敗" };
            }
            return new RegisterResponseDto { IsSuccess = true, Message = "註冊成功" };
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
                return new RegisterResponseDto { IsSuccess = false, Message = "該 email 已被註冊" };
            }

            // 根據後台管理員的選擇，動態傳入對應的角色
            var user = _mapper.Map<UserAccount>(request);
            user.EnPassword = BCrypt.Net.BCrypt.HashPassword(request.Passwd);
            user.UserRole = 1;
            _unitOfWork.UserAccounts.Create(user);
            var result = await _unitOfWork.CompleteAsync();
            if (result < 1)
            {
                return new RegisterResponseDto { IsSuccess = false, Message = "後台帳號建立失敗" };
            }
            return new RegisterResponseDto { IsSuccess = true, Message = "後台帳號建立成功" };
        }

        public async Task<UserDto?> GetUserAsync(int id)
        {
            var user = await _unitOfWork.UserAccounts.GetByIdAsync(id);
            if (user == null)
            {
                return null;
            }
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<int> UpdateUserAsync(UpdateUserDto userDto)
        {
            var user = await _unitOfWork.UserAccounts.GetByIdAsync(userDto.UserAccountId);
            if (user == null)
            {
                return 0;
            }
            
            if(user.UserName != userDto.UserName && userDto.UserName != null)
            {
                user.UserName = userDto.UserName;
            }

            if (user.PhoneNumber != userDto.PhoneNumber && userDto.PhoneNumber != null)
            {
                user.PhoneNumber = userDto.PhoneNumber;
            }

            user.Updatedat = DateTime.Now;

            _unitOfWork.UserAccounts.Update(user);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<int> ChangePasswordAsync(ChangePasswdDto passwdDto)
        {
            var user = await _unitOfWork.UserAccounts.GetByIdAsync(passwdDto.UserAccountId);
            if (user == null)
            {
                return 0;
            }
            var passwdDiff = BCrypt.Net.BCrypt.Verify(passwdDto.CurrentPasswd, user.EnPassword);
            if (!passwdDiff){
                return 0;
            }
            user.EnPassword = BCrypt.Net.BCrypt.HashPassword(passwdDto.NewPasswd);
            user.Updatedat = DateTime.Now;
            _unitOfWork.UserAccounts.Update(user);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<int> DeleteUserAsync(int id)
        {
            var userDto = await _unitOfWork.UserAccounts.GetByIdAsync(id);
            if (userDto == null)
            {
                return 0;
            }
            var user = _mapper.Map<UserAccount>(userDto);
            _unitOfWork.UserAccounts.Delete(user);
            return await _unitOfWork.CompleteAsync();
        }

    }

}
