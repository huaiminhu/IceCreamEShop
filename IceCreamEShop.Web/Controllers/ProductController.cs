using AutoMapper;
using IceCreamEShop.Core.DTOs.Product;
using IceCreamEShop.Service.Services.IServices;
using IceCreamEShop.Web.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IceCreamEShop.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(
            IProductService productService, 
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> DisplayProduct(int id)
        {
            var dto = await _productService.GetProductAsync(id);
            if (dto == null)
            {
                return NotFound("商品不存在！");
            }
            var model = _mapper.Map<DisplayProductViewModel>(dto);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ProductInfo(int id)
        {
            var dto = await _productService.GetProductAsync(id);
            if(dto == null)
            {
                return NotFound("商品不存在！");
            }
            var model = _mapper.Map<ProductViewModel>(dto);
            return View(model);
        }

        [HttpGet]
        //[Authorize(Roles = "EShop_Manager")]
        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        //[Authorize(Roles = "EShop_Manager")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(CreateProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var completeUrl = "";
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if(model.ProductPic != null)
            {
                //使用 Guid 重新命名檔案以避免主機上檔名重複衝突
                string picFileName = Guid.NewGuid().ToString()
                    + Path.GetExtension(model.ProductPic.FileName);

                // 定義產品圖片資料夾路徑
                string path = Path.Combine(wwwRootPath, "images");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                // 上傳的檔案寫入伺服器硬碟
                using (var fileStream = new FileStream(Path.Combine(path, picFileName), FileMode.Create))
                {
                    await model.ProductPic.CopyToAsync(fileStream);
                }

                completeUrl = "/images/" + picFileName;
            }
            var dto = _mapper.Map<ProductDto>(model);
            dto.ProductPicUrl = completeUrl;
            var request = await _productService.CreateProductAsync(dto);
            if (request == 0)
            {
                return BadRequest("商品新增失敗！");
            }
            return RedirectToAction("ProductInfo", new { id = request }); 
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductViewModel model) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var completeUrl = "";
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (model.ProductPic != null)
            {
                string picFileName = Guid.NewGuid().ToString()
                    + Path.GetExtension(model.ProductPic.FileName);

                string path = Path.Combine(wwwRootPath, "images");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (var fileStream = new FileStream(Path.Combine(path, picFileName), FileMode.Create))
                {
                    await model.ProductPic.CopyToAsync(fileStream);
                }

                completeUrl = "/images/" + picFileName;
            }
            var dto = _mapper.Map<UpdateProductDto>(model);
            dto.ProductPicUrl = completeUrl;
            var request = await _productService.UpdateProductAsync(dto);
            if(request == 0)
            {
                return BadRequest("更新失敗！");
            }
            return Json(new { newPicUrl = completeUrl, message = "更新成功！" });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int productId) 
        {
            var result = await _productService.DeleteProductAsync(productId);
            if(result == 0)
            {
                return BadRequest("此商品已被刪除或不存在，請重新整理頁面。");
            }
            return Json(new { message = "刪除成功！" });
        }
    }
}
