using AutoMapper;
using IceCreamEShop.Core.DTOs.Order;
using IceCreamEShop.Service.Services;
using IceCreamEShop.Service.Services.IServices;
using IceCreamEShop.Web.ViewModels.Order;
using IceCreamEShop.Web.ViewModels.Product;
using IceCreamEShop.Web.ViewModels.ShoppingCart;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace IceCreamEShop.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IMapper _mapper;
        public OrderController(IOrderService orderService, 
            IShoppingCartService shoppingCartService, 
            IMapper mapper)
        {
            _orderService = orderService;
            _shoppingCartService = shoppingCartService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody]CreateOrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var orderDto = _mapper.Map<CreateOrderDto>(model);
            var result = await _orderService.CreateOrderAsync(orderDto);
            if(result == 0)
            {
                return BadRequest("訂單建立失敗！");
            }
            return Json(new { successful = true, message = "訂單建立成功！" });
        }

        [HttpGet]
        public async Task<IActionResult> OrderCreated(int id)
        {
            var dto = await _orderService.GetOrderInfoAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<GetOrderViewModel>(dto);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CancelOrder()
        {
            if (!int.TryParse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value, out int userId))
            {
                return BadRequest("訂單取消失敗！");
            }
            var result = await _orderService.CancelOrderAsync(userId);
            if (result == 0) 
            {
                return BadRequest("訂單取消失敗！");
            }
            return Json(new { successful = true, message = "取消成功" });
        }

        [HttpPost]
        public async Task<IActionResult> Payment([FromBody]PaymentRequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var dto = _mapper.Map<PaymentRequestDto>(model);
            dto.RedirectUrls.ConfirmUrl = "https://localhost:44347/Order/LinePayConfirm";
            dto.RedirectUrls.CancelUrl = "https://localhost:44347/Order/OrderCreated";
            var result = await _orderService.PaymentRequestAsync(model.Provider, dto);
            if(result == "Unpaid")
            {
                return Json(new { message = "請找店員結帳" });
            }else if(result == null)
            {
                return BadRequest();
            }
            return Json(new { redirectUrl = result });
        }

        [HttpGet]
        public async Task<IActionResult> LinePayConfirm(LinePayConfirmViewModel model)
        {
            var dto = _mapper.Map<LinePayConfirmDto>(model);
            var isSuccess = await _orderService.ConfirmPaymentAsync(dto);

            if (isSuccess)
            {
                return RedirectToAction("OrderFinished", new { id = dto.OrderId });
            }
            return Json(new { message = "付款失敗！" });
        }

        [HttpPost]
        public async Task<IActionResult> PayByCash(int orderId)
        {
            var result = await _orderService.PayByCashAsync(orderId);
            if(result > 0)
            {
                return Json(new { message = "付款成功！" });
            }
            return BadRequest("付款失敗！");
        }

        [HttpGet]
        public IActionResult OrderFinished(int id)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DisplayUnpaidOrder(string phoneNumber)
        {
            var dto = await _orderService.GetUnpaidOrderAsync(phoneNumber);
            if(dto == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<UnpaidOrderViewModel>(dto);
            return Ok(model);
        }

        [HttpGet]
        public async Task<IActionResult> MyOrders(int userId)
        {
            var dtos = await _orderService.GetUserOrdersAsync(userId);
            var models = _mapper.Map<List<UserOrderViewModel>>(dtos);
            return View(models);
        }
    }
}
