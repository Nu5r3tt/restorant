using RestaurantOrdering.Models;

namespace RestaurantOrdering.Repositories
{
    public interface ITaxRateRepository
    {
        Task<IEnumerable<TaxRate>> GetAllAsync();
        Task<TaxRate?> GetByIdAsync(int id);
        Task<TaxRate> CreateAsync(TaxRate taxRate);
        Task<TaxRate> UpdateAsync(TaxRate taxRate);
        Task DeleteAsync(int id);
        Task<IEnumerable<TaxRate>> GetActiveAsync();
    }
}
