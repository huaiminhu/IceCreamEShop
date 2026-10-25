using AutoMapper;
using IceCreamEShop.Core.Common;
using IceCreamEShop.Core.DTOs.Order;
using IceCreamEShop.Core.DTOs.Product;
using IceCreamEShop.Core.Entities;
using IceCreamEShop.Core.Interfaces;
using IceCreamEShop.Service.Services.IServices;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace IceCreamEShop.Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly LinePaySettings _settings;
        public OrderService(IUnitOfWork unitOfWork, 
            IMapper mapper,
            HttpClient httpClient, 
            IOptions<LinePaySettings> settings)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpClient = httpClient;
            _settings = settings.Value;
        }

        public async Task<int> CancelOrderAsync(int orderId)
        {
            var order = await _unitOfWork.OrderInfos.GetByOrderIdAsync(orderId);
            if (order == null)
            {
                return 0;
            }
            _unitOfWork.OrderInfos.Delete(order);
            _unitOfWork.OrderItems.Delete(orderId);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<int> CreateOrderAsync(CreateOrderDto dto)
        {
            var order = _mapper.Map<OrderInfo>(dto);
            _unitOfWork.OrderInfos.Create(order);
            await _unitOfWork.CompleteAsync();
            
            foreach (var oit in dto.OrderItems)
            {
                var orderItem = _mapper.Map<OrderItem>(oit);
                orderItem.OrderInfoId = order.OrderInfoId;
                var product = await _unitOfWork.Products.GetByIdAsync(oit.ProductId);
                if (product == null)
                {
                    return 0;
                }
                product.Quantity -= oit.Quantity;
                product.UpdatedAt = DateTime.UtcNow;
                _unitOfWork.Products.Update(product);
                _unitOfWork.OrderItems.Create(orderItem);
            }
            var result = await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<GetOrderDto?> GetOrderInfoAsync(int orderId)
        {
            var order = await _unitOfWork.OrderInfos.GetByOrderIdAsync(orderId);
            if(order == null)
            {
                return null;
            }
            var orderItems = new List<GetOrderItemDto>();
            foreach(var it in order.OrderItems)
            {
                var product = await _unitOfWork.Products.GetByIdAsync(it.ProductId);
                if (product == null)
                {
                    return null;
                }
                var productDto = _mapper.Map<ProductDto>(product);
                var dto = new GetOrderItemDto();
                dto.ItemQuantity = it.Quantity;
                dto.OrderItem = productDto;
                orderItems.Add(dto);
            }
            var orderDto = _mapper.Map<GetOrderDto>(order);
            orderDto.OrderItems = orderItems;
            return orderDto;
        }

        public async Task<List<UserOrderDto>> GetUserOrdersAsync(int userId)
        {
            var orders = await _unitOfWork.OrderInfos.GetByUserIdAsync(userId);
            var dtos = _mapper.Map<List<UserOrderDto>>(orders);
            return dtos;
        }

        public async Task<UnpaidOrderDto?> GetUnpaidOrderAsync(string phoneNumber)
        {
            var order = await _unitOfWork.OrderInfos.GetByPhoneNumberAsync(phoneNumber);
            if(order == null)
            {
                return null;
            }
            var dto = _mapper.Map<UnpaidOrderDto>(order);
            return dto;
        }

        public async Task<int> PayByCashAsync(int orderId)
        {
            var order = await _unitOfWork.OrderInfos.GetByOrderIdAsync(orderId);
            if(order == null)
            {
                return 0;
            }
            order.PaymentStatus = 1;
            _unitOfWork.OrderInfos.Update(order);
            return await _unitOfWork.CompleteAsync();
        }
        public async Task<string?> PaymentRequestAsync(string provider, PaymentRequestDto dto)
        {
            var order = await _unitOfWork.OrderInfos.GetByOrderIdAsync(dto.OrderId);
            if (order == null)
            {
                return null;
            }

            if (provider == "Line Pay")
            {
                var paymentEndPoint = "/v4/payments/request";
                var nonce = Guid.NewGuid().ToString();
                var jsonBody = JsonSerializer.Serialize(dto);
                var signature = LinePaySignUtility.CreateSignature(_settings.ChannelSecret, paymentEndPoint, jsonBody, nonce);
                var request = new HttpRequestMessage(HttpMethod.Post, _settings.SandboxUrl + paymentEndPoint);
                request.Headers.Add("X-LINE-ChannelId", _settings.ChannelId);
                request.Headers.Add("X-LINE-Authorization-Nonce", nonce);
                request.Headers.Add("X-LINE-Authorization", signature);
                request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                var response = await _httpClient.SendAsync(request);
                var result = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(result);
                var returnCode = doc.RootElement.GetProperty("returnCode").GetString();
                return returnCode == "0000"? doc.RootElement.GetProperty("info").GetProperty("paymentUrl").GetProperty("web").GetString() : null;
            }else if (provider == "Cash")
            {   
                order.PaymentProvider = "Cash";
                _unitOfWork.OrderInfos.Update(order);
                await _unitOfWork.CompleteAsync();
                return "Unpaid";
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> ConfirmPaymentAsync(LinePayConfirmDto dto)
        {
            var order = await _unitOfWork.OrderInfos.GetByOrderIdAsync(dto.OrderId);
            if (order == null)
            {
                return false;
            }
            var confirmEndPoint = $"/v4/payments/{dto.TransactionId}/confirm";
            var nonce = Guid.NewGuid().ToString();
            var confirmBody = new { amount = order.PaymentAmount, currency = "TWD" };
            var jsonBody = JsonSerializer.Serialize(confirmBody);
            var signature = LinePaySignUtility.CreateSignature(_settings.ChannelSecret, confirmEndPoint, jsonBody, nonce);
            var request = new HttpRequestMessage(HttpMethod.Post, _settings.SandboxUrl + confirmEndPoint);
            request.Headers.Add("X-LINE-ChannelId", _settings.ChannelId);
            request.Headers.Add("X-LINE-Authorization-Nonce", nonce);
            request.Headers.Add("X-LINE-Authorization", signature);
            request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode) 
            {
                return false;
            } 
            var result = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(result);
            var returnCode = doc.RootElement.GetProperty("returnCode").GetString();

            if (returnCode == "0000")
            {
                order.PaymentProvider = "Line Pay";
                order.PaymentStatus = 1;
                _unitOfWork.OrderInfos.Update(order);
                return await _unitOfWork.CompleteAsync() > 0;
            }
            else
            {
                return false;
            }
        }
    }
}