using AutoMapper;
using IceCreamEShop.Core.DTOs.CartItem;
using IceCreamEShop.Core.DTOs.Product;
using IceCreamEShop.Core.DTOs.ShoppingCart;
using IceCreamEShop.Core.Entities;
using IceCreamEShop.Core.Interfaces;
using IceCreamEShop.Service.Services.IServices;

namespace IceCreamEShop.Service.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ShoppingCartService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<int> AddItemToCartAsync(int userAccountId, CreateItemDto dto)
        {
            var cart = await _unitOfWork.ShoppingCarts.GetByAccountIdAsync(userAccountId);
            int cartId;
            if(cart == null)
            {
                var newCart = new ShoppingCart { UserAccountId = userAccountId };
                _unitOfWork.ShoppingCarts.Create(newCart);
                await _unitOfWork.CompleteAsync();
                cartId = newCart.ShoppingCartId;
            }
            else
            {
                cartId = cart.ShoppingCartId;
            }

            var cartItem = _mapper.Map<CartItem>(dto);
            cartItem.ShoppingCartId = cartId;
            var product = await _unitOfWork.Products.GetByIdAsync(cartItem.ProductId);
            if (product == null)
            {
                return 0;
            }
            if (product.Quantity - cartItem.Quantity < 0)
            {
                return 0;
            }
            product.Quantity -= cartItem.Quantity;
            product.UpdatedAt = DateTime.Now;
            _unitOfWork.Products.Update(product);
            _unitOfWork.CartItems.Create(cartItem);
            
            var result = await _unitOfWork.CompleteAsync();
            if(result < 1)
            {
                return 0;
            }
            return result;
        }

        public async Task<GetCartDto?> GetCartAsync(int userAccountId)
        {
            var cart = await _unitOfWork.ShoppingCarts.GetByAccountIdAsync(userAccountId);
            if(cart == null)
            {
                return null;
            }
            var cartItems = await _unitOfWork.CartItems.GetItemsByCartAsync(cart.ShoppingCartId);
            if(cartItems == null)
            {
                return null;
            }
            var cartItemsDto = new List<DisplayItemDto>();
            int totalPrice = 0;
            foreach (var it in cartItems)
            {
                var product = await _unitOfWork.Products.GetByIdAsync(it.ProductId);
                if (product == null)
                {
                    return null;
                }
                var dto = new DisplayItemDto();
                var productDto = _mapper.Map<ProductDto>(product);
                dto.DisplayedItem = productDto;
                dto.ItemQuantity = it.Quantity;
                totalPrice += (it.Quantity * productDto.ProductPrice);
                cartItemsDto.Add(dto);
            }
            var cartDto = _mapper.Map<GetCartDto>(cart);
            cartDto.CartItems = cartItemsDto;
            return cartDto;
        }

        public async Task<int> PutItemsAwayFromCartAsync(int userAccountId, List<RemoveItemDto> dto)
        {
            var cart = await _unitOfWork.ShoppingCarts.GetByAccountIdAsync(userAccountId);
            if (cart == null)
            {
                return 0;
            }
            
            foreach(var it in dto)
            {
                var product = await _unitOfWork.Products.GetByIdAsync(it.ProductId);
                if (product == null)
                {
                    return 0;
                }
                product.Quantity += it.Quantity;
                product.UpdatedAt = DateTime.Now;
                _unitOfWork.Products.Update(product);
                var cartItem = await _unitOfWork.CartItems.GetItemAsync(cart.ShoppingCartId, it.ProductId);
                if(cartItem == null)
                {
                    return 0;
                }
                _unitOfWork.CartItems.Delete(cartItem);
            }
            
            return await _unitOfWork.CompleteAsync();
        }
    }
}
