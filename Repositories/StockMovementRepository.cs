using Microsoft.EntityFrameworkCore;
using RestaurantOrdering.Data;
using RestaurantOrdering.Models;

namespace RestaurantOrdering.Repositories
{
    public class StockMovementRepository : IStockMovementRepository
    {
        private readonly ApplicationDbContext _context;

        public StockMovementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StockMovement>> GetAllAsync()
        {
            return await _context.StockMovements
                .Include(sm => sm.Stock)
                .OrderByDescending(sm => sm.CreatedAt)
                .ToListAsync();
        }

        public async Task<StockMovement?> GetByIdAsync(int id)
        {
            return await _context.StockMovements
                .Include(sm => sm.Stock)
                .FirstOrDefaultAsync(sm => sm.Id == id);
        }

        public async Task<StockMovement> CreateAsync(StockMovement stockMovement)
        {
            stockMovement.CreatedAt = DateTime.Now;
            _context.StockMovements.Add(stockMovement);
            await _context.SaveChangesAsync();
            return stockMovement;
        }

        public async Task<IEnumerable<StockMovement>> GetByStockIdAsync(int stockId)
        {
            return await _context.StockMovements
                .Where(sm => sm.StockId == stockId)
                .OrderByDescending(sm => sm.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<StockMovement>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.StockMovements
                .Include(sm => sm.Stock)
                .Where(sm => sm.CreatedAt >= startDate && sm.CreatedAt <= endDate)
                .OrderByDescending(sm => sm.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<StockMovement>> GetByTypeAsync(StockMovementType type)
        {
            return await _context.StockMovements
                .Include(sm => sm.Stock)
                .Where(sm => sm.Type == type)
                .OrderByDescending(sm => sm.CreatedAt)
                .ToListAsync();
        }

        public async Task<bool> ProcessStockMovementAsync(int stockId, StockMovementType type, decimal quantity, 
            string? notes = null, string? reference = null, decimal? unitPrice = null, string? supplier = null)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var stock = await _context.Stocks.FindAsync(stockId);
                if (stock == null) return false;

                // Stok hareketi kaydet
                var movement = new StockMovement
                {
                    StockId = stockId,
                    Type = type,
                    Quantity = quantity,
                    Notes = notes,
                    Reference = reference,
                    UnitPrice = unitPrice,
                    Supplier = supplier,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "Admin" // TODO: Gerçek kullanıcıdan al
                };

                _context.StockMovements.Add(movement);

                // Stok miktarını güncelle
                switch (type)
                {
                    case StockMovementType.StockIn:
                    case StockMovementType.Adjustment when quantity > 0:
                        stock.CurrentQuantity += quantity;
                        break;
                    case StockMovementType.StockOut:
                    case StockMovementType.Waste:
                        stock.CurrentQuantity = Math.Max(0, stock.CurrentQuantity - quantity);
                        break;
                    case StockMovementType.Adjustment when quantity < 0:
                        stock.CurrentQuantity = Math.Max(0, stock.CurrentQuantity + quantity); // quantity negatif olduğu için +
                        break;
                }

                stock.LastUpdated = DateTime.Now;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
    }
}
