using IceCreamEShop.Core.DTOs.Order;

namespace IceCreamEShop.Service.Services.IServices
{
    public interface IOrderService
    {
        Task<int> CreateOrderAsync(CreateOrderDto dto);
        Task<int> CancelOrderAsync(int orderId);
        Task<GetOrderDto?> GetOrderInfoAsync(int orderId);
        Task<UnpaidOrderDto?> GetUnpaidOrderAsync(string phoneNumber);
        Task<List<UserOrderDto>> GetUserOrdersAsync(int userId);
        Task<int> PayByCashAsync(int orderId);
        Task<string?> PaymentRequestAsync(string provider, PaymentRequestDto dto);
        Task<bool> ConfirmPaymentAsync(LinePayConfirmDto dto);
    }
}
