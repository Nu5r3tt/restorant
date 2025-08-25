using RestaurantOrdering.Models;

namespace RestaurantOrdering.Repositories
{
    public interface IStockMovementRepository
    {
        Task<IEnumerable<StockMovement>> GetAllAsync();
        Task<StockMovement?> GetByIdAsync(int id);
        Task<StockMovement> CreateAsync(StockMovement stockMovement);
        Task<IEnumerable<StockMovement>> GetByStockIdAsync(int stockId);
        Task<IEnumerable<StockMovement>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<StockMovement>> GetByTypeAsync(StockMovementType type);
        Task<bool> ProcessStockMovementAsync(int stockId, StockMovementType type, decimal quantity, string? notes = null, string? reference = null, decimal? unitPrice = null, string? supplier = null);
    }
}
