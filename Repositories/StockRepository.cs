using Microsoft.EntityFrameworkCore;
using RestaurantOrdering.Data;
using RestaurantOrdering.Models;

namespace RestaurantOrdering.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Stock>> GetAllAsync()
        {
            return await _context.Stocks
                .Include(s => s.StockMovements)
                .Where(s => s.IsActive)
                .OrderBy(s => s.Name)
                .ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks
                .Include(s => s.StockMovements)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Stock> CreateAsync(Stock stock)
        {
            stock.CreatedAt = DateTime.Now;
            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock> UpdateAsync(Stock stock)
        {
            stock.LastUpdated = DateTime.Now;
            _context.Entry(stock).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var stock = await GetByIdAsync(id);
            if (stock == null) return false;

            stock.IsActive = false;
            stock.LastUpdated = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Stock>> GetLowStockItemsAsync()
        {
            return await _context.Stocks
                .Where(s => s.IsActive && s.CurrentQuantity <= s.MinimumQuantity)
                .OrderBy(s => s.CurrentQuantity / s.MinimumQuantity)
                .ToListAsync();
        }

        public async Task<IEnumerable<Stock>> GetByCategoryAsync(StockCategory category)
        {
            return await _context.Stocks
                .Where(s => s.IsActive && s.Category == category)
                .OrderBy(s => s.Name)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Stocks.AnyAsync(s => s.Id == id && s.IsActive);
        }

        public async Task<IEnumerable<Stock>> SearchAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllAsync();

            searchTerm = searchTerm.ToLower();
            return await _context.Stocks
                .Include(s => s.Supplier)
                .Include(s => s.TaxRate)
                .Where(s => s.IsActive && 
                           (s.Name.ToLower().Contains(searchTerm) || 
                            s.Description.ToLower().Contains(searchTerm) ||
                            (s.Supplier != null && s.Supplier.Name.ToLower().Contains(searchTerm))))
                .OrderBy(s => s.Name)
                .ToListAsync();
        }
    }
}
