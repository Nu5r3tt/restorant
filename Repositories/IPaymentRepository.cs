using RestaurantOrdering.Models;

namespace RestaurantOrdering.Repositories
{
    public interface IPaymentRepository
    {
        Task<Payment> CreateAsync(Payment payment);
        Task<Payment?> GetByIdAsync(int id);
        Task<IEnumerable<Payment>> GetAllAsync();
        Task<IEnumerable<Payment>> GetByTableIdAsync(int tableId);
        Task<IEnumerable<Payment>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Order>> GetUnpaidOrdersByTableAsync(int tableId);
        Task<decimal> GetTableTotalAsync(int tableId);
        Task<decimal> GetTotalRevenueAsync();
        Task<decimal> GetTotalRevenueByDateAsync(DateTime date);
        Task<int> GetTotalTransactionsAsync();
        Task<int> GetTodayTransactionsAsync();
        Task<bool> HasUnpaidOrdersAsync(int tableId);
        Task<IEnumerable<Payment>> GetPendingPaymentsAsync();
        Task<Payment> UpdateAsync(Payment payment);
        Task DeleteAsync(int id);
        Task<PaymentItem> CreatePaymentItemAsync(PaymentItem paymentItem);
    }
}
