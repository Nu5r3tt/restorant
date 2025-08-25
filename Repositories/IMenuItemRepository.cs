using RestaurantOrdering.Models;

namespace RestaurantOrdering.Repositories
{
    public interface IMenuItemRepository
    {
        Task<IEnumerable<MenuItem>> GetAllAsync();
        Task<IEnumerable<MenuItem>> GetByMenuIdAsync(int menuId);
        Task<MenuItem?> GetByIdAsync(int id);
        Task<MenuItem> CreateAsync(MenuItem menuItem);
        Task UpdateAsync(MenuItem menuItem);
        Task DeleteAsync(int id);
    }
}
