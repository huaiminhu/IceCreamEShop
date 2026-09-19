using AutoMapper;
using IceCreamEShop.Core.DTOs.CartItem;
using IceCreamEShop.Service.Services.IServices;
using IceCreamEShop.Web.ViewModels.CartItem;
using IceCreamEShop.Web.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;

namespace IceCreamEShop.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ShoppingCartController(
            IShoppingCartService shoppingCartService,
            IProductService productService, 
            IMapper mapper)
        {
            _shoppingCartService = shoppingCartService;
            _productService = productService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> MyCart()
        {
            if (!int.TryParse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value, out int userId))
            {
                return BadRequest();
            }
            var cart = await _shoppingCartService.GetCartItemsAsync(userId);
            if (cart == null)
            {
                return BadRequest();
            }
            var model = _mapper.Map<List<DisplayProductViewModel>>(cart);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(CreateItemViewModel model)
        {
            if (!int.TryParse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value, out int userId))
            {
                return BadRequest("使用者不存在！");
            }
            var dto = _mapper.Map<CreateItemDto>(model);
            var addResult = await _shoppingCartService.AddItemToCartAsync(userId, dto);
            if (addResult == 0)
            {
                return BadRequest("新增失敗！");
            }
            return Json(new { success = 1, message = "新增成功！" });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveItem([FromBody]List<RemoveItemViewModel> model)
        {
            if (!int.TryParse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value, out int userId))
            {
                return BadRequest();
            }
            var dto = _mapper.Map<List<RemoveItemDto>>(model);
            var removeResult = await _shoppingCartService.PutItemsAwayFromCartAsync(userId, dto);
            if (removeResult == 0)
            {
                return BadRequest();
            }
            return Json(new { message = "移除成功！" });
        }
    }
}
