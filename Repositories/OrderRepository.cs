using Microsoft.EntityFrameworkCore;
using RestaurantOrdering.Data;
using RestaurantOrdering.Models;

namespace RestaurantOrdering.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(o => o.Table)
                .Include(o => o.MenuItem)
                .ThenInclude(mi => mi.Menu)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetByTableIdAsync(int tableId)
        {
            return await _context.Orders
                .Include(o => o.MenuItem)
                .ThenInclude(mi => mi.Menu)
                .Where(o => o.TableId == tableId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetPendingOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.Table)
                .Include(o => o.MenuItem)
                .ThenInclude(mi => mi.Menu)
                .Where(o => o.Status == OrderStatus.Pending || o.Status == OrderStatus.Confirmed || o.Status == OrderStatus.Preparing)
                .OrderBy(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.Table)
                .Include(o => o.MenuItem)
                .ThenInclude(mi => mi.Menu)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Order> CreateAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> UpdateAsync(Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<bool> UpdateStatusAsync(int id, OrderStatus status)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
                return false;

            order.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
                return false;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<decimal> GetTotalSalesAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = _context.Orders.Where(o => o.Status == OrderStatus.Served);

            if (startDate.HasValue)
                query = query.Where(o => o.OrderDate >= startDate);

            if (endDate.HasValue)
                query = query.Where(o => o.OrderDate <= endDate);

            return await query.SumAsync(o => o.TotalPrice);
        }

        public async Task<IEnumerable<Order>> GetPendingOrdersByTableIdAsync(int tableId)
        {
            return await _context.Orders
                .Include(o => o.MenuItem)
                    .ThenInclude(mi => mi.Menu)
                .Include(o => o.Table)
                .Where(o => o.TableId == tableId && 
                           (o.Status == OrderStatus.Pending || 
                            o.Status == OrderStatus.Confirmed || 
                            o.Status == OrderStatus.Preparing || 
                            o.Status == OrderStatus.Ready))
                .OrderBy(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<Dictionary<int, int>> GetPendingOrderCountsByTableAsync()
        {
            return await _context.Orders
                .Where(o => o.Status == OrderStatus.Pending || 
                           o.Status == OrderStatus.Confirmed || 
                           o.Status == OrderStatus.Preparing || 
                           o.Status == OrderStatus.Ready)
                .GroupBy(o => o.TableId)
                .ToDictionaryAsync(g => g.Key, g => g.Count());
        }
    }
}
