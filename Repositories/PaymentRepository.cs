using Microsoft.EntityFrameworkCore;
using RestaurantOrdering.Data;
using RestaurantOrdering.Models;

namespace RestaurantOrdering.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Payment>> GetAllAsync()
        {
            return await _context.Payments
                .Include(p => p.Table)
                .Include(p => p.PaymentItems)
                    .ThenInclude(pi => pi.Order)
                        .ThenInclude(o => o.MenuItem)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<Payment?> GetByIdAsync(int id)
        {
            return await _context.Payments
                .Include(p => p.Table)
                .Include(p => p.PaymentItems)
                    .ThenInclude(pi => pi.Order)
                        .ThenInclude(o => o.MenuItem)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Payment>> GetByTableIdAsync(int tableId)
        {
            return await _context.Payments
                .Include(p => p.Table)
                .Include(p => p.PaymentItems)
                    .ThenInclude(pi => pi.Order)
                        .ThenInclude(o => o.MenuItem)
                .Where(p => p.TableId == tableId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Payment>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Payments
                .Include(p => p.Table)
                .Include(p => p.PaymentItems)
                .Where(p => p.CreatedAt >= startDate && p.CreatedAt <= endDate)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<Payment> CreateAsync(Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            return payment;
        }

        public async Task<Payment> UpdateAsync(Payment payment)
        {
            _context.Entry(payment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return payment;
        }

        public async Task DeleteAsync(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<decimal> GetTotalRevenueAsync(DateTime? date = null)
        {
            var query = _context.Payments
                .Where(p => p.Status == PaymentStatus.Completed);

            if (date.HasValue)
            {
                var startOfDay = date.Value.Date;
                var endOfDay = startOfDay.AddDays(1);
                query = query.Where(p => p.CompletedAt >= startOfDay && p.CompletedAt < endOfDay);
            }

            return await query.SumAsync(p => p.TotalAmount);
        }

        public async Task<IEnumerable<Payment>> GetTodaysPaymentsAsync()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            return await _context.Payments
                .Include(p => p.Table)
                .Include(p => p.PaymentItems)
                .Where(p => p.CreatedAt >= today && p.CreatedAt < tomorrow)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<bool> HasUnpaidOrdersAsync(int tableId)
        {
            return await _context.Orders
                .AnyAsync(o => o.TableId == tableId && 
                              o.Status == OrderStatus.Served && 
                              !_context.PaymentItems.Any(pi => pi.OrderId == o.Id));
        }

        public async Task<decimal> GetTableTotalAsync(int tableId)
        {
            return await _context.Orders
                .Where(o => o.TableId == tableId && 
                           o.Status == OrderStatus.Served && 
                           !_context.PaymentItems.Any(pi => pi.OrderId == o.Id))
                .SumAsync(o => o.TotalPrice);
        }

        public async Task<IEnumerable<Order>> GetUnpaidOrdersByTableAsync(int tableId)
        {
            return await _context.Orders
                .Include(o => o.MenuItem)
                .Where(o => o.TableId == tableId && 
                           o.Status == OrderStatus.Served && 
                           !_context.PaymentItems.Any(pi => pi.OrderId == o.Id))
                .OrderBy(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<decimal> GetTotalRevenueAsync()
        {
            return await _context.Payments
                .Where(p => p.Status == PaymentStatus.Completed)
                .SumAsync(p => p.TotalAmount);
        }

        public async Task<decimal> GetTotalRevenueByDateAsync(DateTime date)
        {
            var startDate = date.Date;
            var endDate = startDate.AddDays(1);

            return await _context.Payments
                .Where(p => p.Status == PaymentStatus.Completed &&
                           p.CompletedAt >= startDate &&
                           p.CompletedAt < endDate)
                .SumAsync(p => p.TotalAmount);
        }

        public async Task<int> GetTotalTransactionsAsync()
        {
            return await _context.Payments
                .Where(p => p.Status == PaymentStatus.Completed)
                .CountAsync();
        }

        public async Task<int> GetTodayTransactionsAsync()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            return await _context.Payments
                .Where(p => p.Status == PaymentStatus.Completed &&
                           p.CompletedAt >= today &&
                           p.CompletedAt < tomorrow)
                .CountAsync();
        }

        public async Task<IEnumerable<Payment>> GetPendingPaymentsAsync()
        {
            return await _context.Payments
                .Include(p => p.Table)
                .Where(p => p.Status == PaymentStatus.Pending || p.Status == PaymentStatus.Processing)
                .OrderBy(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<PaymentItem> CreatePaymentItemAsync(PaymentItem paymentItem)
        {
            _context.PaymentItems.Add(paymentItem);
            await _context.SaveChangesAsync();
            return paymentItem;
        }
    }
}
