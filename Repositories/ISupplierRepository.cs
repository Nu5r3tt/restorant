using RestaurantOrdering.Models;

namespace RestaurantOrdering.Repositories
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> GetAllAsync();
        Task<Supplier?> GetByIdAsync(int id);
        Task<Supplier> CreateAsync(Supplier supplier);
        Task<Supplier> UpdateAsync(Supplier supplier);
        Task DeleteAsync(int id);
        Task<IEnumerable<Supplier>> GetActiveAsync();
    }
}
