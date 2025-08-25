using Microsoft.EntityFrameworkCore;
using RestaurantOrdering.Data;
using RestaurantOrdering.Models;

namespace RestaurantOrdering.Repositories
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly ApplicationDbContext _context;

        public MenuItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MenuItem>> GetAllAsync()
        {
            return await _context.MenuItems
                .Include(mi => mi.Menu)
                .OrderBy(mi => mi.Menu.Name)
                .ThenBy(mi => mi.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<MenuItem>> GetByMenuIdAsync(int menuId)
        {
            return await _context.MenuItems
                .Where(mi => mi.MenuId == menuId)
                .OrderBy(mi => mi.Name)
                .ToListAsync();
        }

        public async Task<MenuItem?> GetByIdAsync(int id)
        {
            return await _context.MenuItems
                .Include(mi => mi.Menu)
                .FirstOrDefaultAsync(mi => mi.Id == id);
        }

        public async Task<MenuItem> CreateAsync(MenuItem menuItem)
        {
            // AGGRESSIVE SAFETY FIX: Force correct data assignment
            Console.WriteLine($"🔍 BEFORE SAFETY FIX - ImageUrl: '{menuItem.ImageUrl?.Substring(0, Math.Min(50, menuItem.ImageUrl?.Length ?? 0))}', ImageData: '{menuItem.ImageData?.Substring(0, Math.Min(50, menuItem.ImageData?.Length ?? 0))}'");
            
            // Safety mechanism: If ImageUrl contains Base64 data, move it to ImageData
            if (!string.IsNullOrEmpty(menuItem.ImageUrl) && menuItem.ImageUrl.StartsWith("data:"))
            {
                // Move Base64 data from ImageUrl to ImageData
                menuItem.ImageData = menuItem.ImageUrl;
                menuItem.ImageUrl = "";
                
                // Log the correction
                Console.WriteLine("⚠️ SAFETY FIX: Moved Base64 data from ImageUrl to ImageData");
            }
            
            // Double check: Ensure ImageUrl doesn't contain Base64 data
            if (!string.IsNullOrEmpty(menuItem.ImageUrl) && menuItem.ImageUrl.Length > 100)
            {
                Console.WriteLine("⚠️ AGGRESSIVE SAFETY FIX: ImageUrl too long, clearing it");
                menuItem.ImageUrl = "";
            }
            
            // Force empty ImageUrl if we have ImageData
            if (!string.IsNullOrEmpty(menuItem.ImageData))
            {
                Console.WriteLine("🔧 FORCING ImageUrl to empty because ImageData exists");
                menuItem.ImageUrl = "";
            }
            
            Console.WriteLine($"✅ AFTER SAFETY FIX - ImageUrl: '{menuItem.ImageUrl}', ImageData exists: {!string.IsNullOrEmpty(menuItem.ImageData)}");
            
            _context.MenuItems.Add(menuItem);
            await _context.SaveChangesAsync();
            return menuItem;
        }

        public async Task UpdateAsync(MenuItem menuItem)
        {
            // AGGRESSIVE SAFETY FIX: Force correct data assignment
            Console.WriteLine($"🔍 UPDATE BEFORE SAFETY FIX - ImageUrl: '{menuItem.ImageUrl?.Substring(0, Math.Min(50, menuItem.ImageUrl?.Length ?? 0))}', ImageData: '{menuItem.ImageData?.Substring(0, Math.Min(50, menuItem.ImageData?.Length ?? 0))}'");
            
            // Safety mechanism: If ImageUrl contains Base64 data, move it to ImageData
            if (!string.IsNullOrEmpty(menuItem.ImageUrl) && menuItem.ImageUrl.StartsWith("data:"))
            {
                // Move Base64 data from ImageUrl to ImageData
                menuItem.ImageData = menuItem.ImageUrl;
                menuItem.ImageUrl = "";
                
                // Log the correction
                Console.WriteLine("⚠️ UPDATE SAFETY FIX: Moved Base64 data from ImageUrl to ImageData");
            }
            
            // Double check: Ensure ImageUrl doesn't contain Base64 data
            if (!string.IsNullOrEmpty(menuItem.ImageUrl) && menuItem.ImageUrl.Length > 100)
            {
                Console.WriteLine("⚠️ UPDATE AGGRESSIVE SAFETY FIX: ImageUrl too long, clearing it");
                menuItem.ImageUrl = "";
            }
            
            // Force empty ImageUrl if we have ImageData
            if (!string.IsNullOrEmpty(menuItem.ImageData))
            {
                Console.WriteLine("🔧 UPDATE FORCING ImageUrl to empty because ImageData exists");
                menuItem.ImageUrl = "";
            }
            
            Console.WriteLine($"✅ UPDATE AFTER SAFETY FIX - ImageUrl: '{menuItem.ImageUrl}', ImageData exists: {!string.IsNullOrEmpty(menuItem.ImageData)}");
            
            _context.MenuItems.Update(menuItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem != null)
            {
                // İlgili siparişleri kontrol et
                var hasOrders = await _context.Orders
                    .AnyAsync(o => o.MenuItemId == id);
                
                if (hasOrders)
                {
                    throw new InvalidOperationException("Bu menü öğesi silinemez çünkü sipariş geçmişinde kullanılmıştır. Bunun yerine öğeyi 'Mevcut Değil' olarak işaretleyebilirsiniz.");
                }
                
                _context.MenuItems.Remove(menuItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}
