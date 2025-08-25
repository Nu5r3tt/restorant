using RestaurantOrdering.Models;

namespace RestaurantOrdering.Repositories
{
    public interface IStockRepository
    {
        Task<IEnumerable<Stock>> GetAllAsync();
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> CreateAsync(Stock stock);
        Task<Stock> UpdateAsync(Stock stock);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Stock>> GetLowStockItemsAsync();
        Task<IEnumerable<Stock>> GetByCategoryAsync(StockCategory category);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<Stock>> SearchAsync(string searchTerm);
    }
}
