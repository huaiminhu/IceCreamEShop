using AutoMapper;
using IceCreamEShop.Core.Common;
using IceCreamEShop.Core.DTOs.Product;
using IceCreamEShop.Core.Entities;
using IceCreamEShop.Core.Interfaces;
using IceCreamEShop.Service.Services.IServices;

namespace IceCreamEShop.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<int> CreateProductAsync(ProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            _unitOfWork.Products.Create(product);
            var result = await _unitOfWork.CompleteAsync();
            if (result < 1)
            {
                return 0;
            }
            return product.ProductId;
        }

        public async Task<int> DeleteProductAsync(int productId)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(productId);
            var dto = _mapper.Map<Product>(product);
            _unitOfWork.Products.Delete(dto);
            var result = await _unitOfWork.CompleteAsync();
            if(result < 1)
            {
                return 0;
            }
            return result;
        }

        public async Task<PageObject<ProductDto>?> GetPageProductsAsync(int pageNumber, int pageSize)
        {
            var products = await _unitOfWork.Products.GetPageProductsAsync(pageNumber, pageSize);
            if(products == null)
            {
                return null;
            }
            var dtos = _mapper.Map<IEnumerable<ProductDto>>(products.Items);
            var response = new PageObject<ProductDto>
            {
                Items = dtos, 
                TotalCount = products.TotalCount, 
                PageNumber = products.PageNumber, 
                PageSize = products.PageSize
            };
            return response;
        }

        public async Task<ProductDto?> GetProductAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if(product == null)
            {
                return null;
            }
            var dto = _mapper.Map<ProductDto>(product);
            return dto;
        }

        public async Task<int> UpdateProductAsync(UpdateProductDto dto)
        {
            var existProduct = await _unitOfWork.Products.GetByIdAsync(dto.ProductId);
            if (existProduct == null)
            {
                return 0;
            }

            var updatedProduct = _mapper.Map(dto, existProduct);
            updatedProduct.UpdatedAt = DateTime.Now;
            _unitOfWork.Products.Update(updatedProduct);
            var result = await _unitOfWork.CompleteAsync();
            return result;
        }
    }
}
