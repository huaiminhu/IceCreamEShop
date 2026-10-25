using IceCreamEShop.Core.Entities;

namespace IceCreamEShop.Core.Interfaces
{
    public interface IOrderInfoRepository
    {
        void Create(OrderInfo order);
        Task<OrderInfo?> GetByOrderIdAsync(int OrderId);
        Task<OrderInfo?> GetByPhoneNumberAsync(string phoneNumber);
        Task<List<OrderInfo>> GetByUserIdAsync(int userId);
        void Update(OrderInfo order);
        void Delete(OrderInfo order);
    }
}
