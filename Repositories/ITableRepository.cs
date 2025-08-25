using RestaurantOrdering.Models;

namespace RestaurantOrdering.Repositories
{
    public interface ITableRepository
    {
        Task<IEnumerable<Table>> GetAllAsync();
        Task<IEnumerable<Table>> GetActiveTablesAsync();
        Task<Table?> GetByIdAsync(int id);
        Task<Table?> GetByTableNumberAsync(int tableNumber);
        Task<Table> CreateAsync(Table table);
        Task<Table> UpdateAsync(Table table);
        Task<bool> DeleteAsync(int id);
    }
}
