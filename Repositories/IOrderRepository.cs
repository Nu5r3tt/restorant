using RestaurantOrdering.Models;

namespace RestaurantOrdering.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<IEnumerable<Order>> GetByTableIdAsync(int tableId);
        Task<IEnumerable<Order>> GetPendingOrdersAsync();
        Task<IEnumerable<Order>> GetPendingOrdersByTableIdAsync(int tableId);
        Task<Dictionary<int, int>> GetPendingOrderCountsByTableAsync();
        Task<Order?> GetByIdAsync(int id);
        Task<Order> CreateAsync(Order order);
        Task<Order> UpdateAsync(Order order);
        Task<bool> UpdateStatusAsync(int id, OrderStatus status);
        Task<bool> DeleteAsync(int id);
        Task<decimal> GetTotalSalesAsync(DateTime? startDate = null, DateTime? endDate = null);
    }
}
