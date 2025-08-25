using RestaurantOrdering.Models;

namespace RestaurantOrdering.Repositories
{
    public interface IMenuRepository
    {
        Task<IEnumerable<Menu>> GetAllAsync();
        Task<IEnumerable<Menu>> GetAllWithItemsAsync();
        Task<IEnumerable<Menu>> GetActiveMenusAsync();
        Task<Menu?> GetByIdAsync(int id);
        Task<Menu?> GetByIdWithItemsAsync(int id);
        Task<Menu> CreateAsync(Menu menu);
        Task<Menu> UpdateAsync(Menu menu);
        Task<bool> DeleteAsync(int id);
    }
}
