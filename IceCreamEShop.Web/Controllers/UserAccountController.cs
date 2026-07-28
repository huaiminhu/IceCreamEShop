using AutoMapper;
using IceCreamEShop.Core.DTOs.UserAccount;
using IceCreamEShop.Core.Enums;
using IceCreamEShop.Service.Services.IServices;
using IceCreamEShop.Web.ViewModels.UserAccount;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IceCreamEShop.Web.Controllers
{
    public class UserAccountController : Controller
    {
        private readonly IUserAccountService _userAccountService;
        private readonly IMapper _mapper;
        public UserAccountController(IUserAccountService userAccountService
                                                       ,IMapper mapper)
        {
            _userAccountService = userAccountService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LoginViewModel model, bool rememberMe)
        {
            if (!ModelState.IsValid) 
            {
                return View(model);
            } 

            var request = _mapper.Map<LoginRequestDto>(model);

            // 1. 呼叫 BLL 驗證帳密
            var user = await _userAccountService.AuthenticateAsync(request);
            if (user == null)
            {
                ModelState.AddModelError("", "帳號或密碼錯誤");
                return View(model);
            }

            // 2. 建立使用者的 Claims (權限與識別證內容)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserAccountId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.UserRole)
            };

            // 3. 打包成身分識別證
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties { IsPersistent = false }; 
            if (rememberMe)
            {
                authProperties.IsPersistent = true; // 記住我功能
            }

            // 4. 寫入 Cookie，完成登入
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            if(user.UserRole == "General_User")
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Admin", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // 清除瀏覽器的驗證 Cookie
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (string.IsNullOrEmpty(model.PhoneNumber))
            {
                model.PhoneNumber = "";
            }

            var request = _mapper.Map<RegisterRequestDto>(model);

            var response = await _userAccountService.RegisterAsync(request);

            if (!response.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, response.Message);
                return View(model);
            }

            TempData["SuccessMessage"] = response.Message;
            return RedirectToAction("Login");
        }

        [HttpGet]
        [Authorize(Roles = "Sys_Manager")]
        public IActionResult AddAdmin()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Sys_Manager")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAdmin(RegisterViewModel model, UserRole targetRole)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (string.IsNullOrEmpty(model.PhoneNumber))
            {
                model.PhoneNumber = "";
            }

            // 從目前登入的 Cookie 內直接撈出 Role Claim
            var roleClaim = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

            // roleClaim字串轉成 UserRole Enum
            if (!Enum.TryParse(roleClaim, out UserRole currentRole))
            {
                return Forbid(); // 解析失敗直接回傳 403 拒絕存取
            }

            var request = _mapper.Map<RegisterRequestDto>(model);

            var response = await _userAccountService.CreateUserByAdminAsync(request, currentRole, targetRole);

            if (!response.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, response.Message);
                return View(model);
            }

            TempData["SuccessMessage"] = response.Message;
            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> UserInfo()
        {
            if (int.TryParse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value, out int userId))
            {
                var user = await _userAccountService.GetUserAsync(userId);
                if (user == null)
                {
                    return NotFound();
                }
                var userModel = _mapper.Map<UserViewModel>(user);
                return View(userModel);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUser(UpdateUserViewModel model)
        {
            if (!ModelState.IsValid) {
                return BadRequest();
            }
            var dto = _mapper.Map<UpdateUserDto>(model);
            if (int.TryParse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value, out int userId))
            {
                dto.UserAccountId = userId;
                var result = await _userAccountService.UpdateUserAsync(dto);
                if (result < 1)
                {
                    return BadRequest();
                }

                // 重新發行 Cookie 以更新畫面的名字
                var identity = (ClaimsIdentity)User.Identity;

                // 移除舊的姓名宣告
                var existingNameClaim = identity.FindFirst(ClaimTypes.Name);
                if (existingNameClaim != null) identity.RemoveClaim(existingNameClaim);

                // 加入新的姓名宣告（使用前端傳入或資料庫最新的名字）
                identity.AddClaim(new Claim(ClaimTypes.Name, model.UserName));

                // 重新寫入瀏覽器 Cookie
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return Ok();
            }
            else{
                return BadRequest();
            }  
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePasswd(ChangePasswdViewModel model)
        {
            if (!ModelState.IsValid || model.CurrentPasswd == model.NewPasswd || model.NewPasswd != model.ComfirmPasswd)
            {
                return BadRequest();
            }
            var dto = _mapper.Map<ChangePasswdDto>(model);
            if (int.TryParse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value, out int userId))
            {
                dto.UserAccountId = userId;
                var result = await _userAccountService.ChangePasswordAsync(dto);
                if (result < 1)
                {
                    return BadRequest();
                }
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser()
        {
            if (int.TryParse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value, out int userId))
            {
                var result = await _userAccountService.DeleteUserAsync(userId);
                if (result < 1)
                {
                    return BadRequest();
                }
                // 刪除成功後，清除登入 Cookie
                await HttpContext.SignOutAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
