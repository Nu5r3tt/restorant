using Microsoft.EntityFrameworkCore;
using RestaurantOrdering.Data;
using RestaurantOrdering.Models;

namespace RestaurantOrdering.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly ApplicationDbContext _context;

        public MenuRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Menu>> GetAllAsync()
        {
            return await _context.Menus
                .OrderBy(m => m.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Menu>> GetAllWithItemsAsync()
        {
            return await _context.Menus
                .Include(m => m.MenuItems)
                .OrderBy(m => m.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Menu>> GetActiveMenusAsync()
        {
            return await _context.Menus
                .Include(m => m.MenuItems.Where(mi => mi.IsAvailable))
                .Where(m => m.IsActive)
                .OrderBy(m => m.Name)
                .ToListAsync();
        }

        public async Task<Menu?> GetByIdAsync(int id)
        {
            return await _context.Menus.FindAsync(id);
        }

        public async Task<Menu?> GetByIdWithItemsAsync(int id)
        {
            return await _context.Menus
                .Include(m => m.MenuItems)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Menu> CreateAsync(Menu menu)
        {
            _context.Menus.Add(menu);
            await _context.SaveChangesAsync();
            return menu;
        }

        public async Task<Menu> UpdateAsync(Menu menu)
        {
            _context.Entry(menu).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return menu;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu == null)
                return false;

            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
