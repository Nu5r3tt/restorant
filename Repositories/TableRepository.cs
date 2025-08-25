using Microsoft.EntityFrameworkCore;
using RestaurantOrdering.Data;
using RestaurantOrdering.Models;

namespace RestaurantOrdering.Repositories
{
    public class TableRepository : ITableRepository
    {
        private readonly ApplicationDbContext _context;

        public TableRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Table>> GetAllAsync()
        {
            return await _context.Tables
                .OrderBy(t => t.TableNumber)
                .ToListAsync();
        }

        public async Task<IEnumerable<Table>> GetActiveTablesAsync()
        {
            return await _context.Tables
                .Where(t => t.IsActive)
                .OrderBy(t => t.TableNumber)
                .ToListAsync();
        }

        public async Task<Table?> GetByIdAsync(int id)
        {
            return await _context.Tables.FindAsync(id);
        }

        public async Task<Table?> GetByTableNumberAsync(int tableNumber)
        {
            return await _context.Tables
                .FirstOrDefaultAsync(t => t.TableNumber == tableNumber);
        }

        public async Task<Table> CreateAsync(Table table)
        {
            _context.Tables.Add(table);
            await _context.SaveChangesAsync();
            return table;
        }

        public async Task<Table> UpdateAsync(Table table)
        {
            _context.Entry(table).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return table;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var table = await _context.Tables.FindAsync(id);
            if (table == null)
                return false;

            _context.Tables.Remove(table);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
