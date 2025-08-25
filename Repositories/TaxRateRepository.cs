using Microsoft.EntityFrameworkCore;
using RestaurantOrdering.Data;
using RestaurantOrdering.Models;

namespace RestaurantOrdering.Repositories
{
    public class TaxRateRepository : ITaxRateRepository
    {
        private readonly ApplicationDbContext _context;

        public TaxRateRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaxRate>> GetAllAsync()
        {
            return await _context.TaxRates
                .OrderBy(t => t.Rate)
                .ToListAsync();
        }

        public async Task<TaxRate?> GetByIdAsync(int id)
        {
            return await _context.TaxRates
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TaxRate> CreateAsync(TaxRate taxRate)
        {
            _context.TaxRates.Add(taxRate);
            await _context.SaveChangesAsync();
            return taxRate;
        }

        public async Task<TaxRate> UpdateAsync(TaxRate taxRate)
        {
            _context.TaxRates.Update(taxRate);
            await _context.SaveChangesAsync();
            return taxRate;
        }

        public async Task DeleteAsync(int id)
        {
            var taxRate = await GetByIdAsync(id);
            if (taxRate != null)
            {
                _context.TaxRates.Remove(taxRate);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TaxRate>> GetActiveAsync()
        {
            return await _context.TaxRates
                .Where(t => t.IsActive)
                .OrderBy(t => t.Rate)
                .ToListAsync();
        }
    }
}
