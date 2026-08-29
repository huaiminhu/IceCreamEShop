using System.Diagnostics;
using AutoMapper;
using IceCreamEShop.Core.Common;
using IceCreamEShop.Service.Services.IServices;
using IceCreamEShop.Web.Models;
using IceCreamEShop.Web.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;

namespace IceCreamEShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ILogger<HomeController> _logger;

        public HomeController( 
            IProductService productService, 
            IMapper mapper,
            ILogger<HomeController> logger)
        {
            _productService = productService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageNum = 1)
        {
            int pageSize = 20;

            var dtos = await _productService.GetPageProductsAsync(pageNum, pageSize);
            if (dtos == null)
            {
                return NotFound();
            }
            var models = _mapper.Map<IEnumerable<ProductViewModel>>(dtos.Items);
            var products = new PageObject<ProductViewModel>
            {
                Items = models,
                TotalCount = dtos.TotalCount,
                PageNumber = dtos.PageNumber,
                PageSize = dtos.PageSize
            };
            return View(products);
            //return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Admin()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
