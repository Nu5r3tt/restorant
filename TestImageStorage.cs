using Microsoft.EntityFrameworkCore;
using RestaurantOrdering.Data;
using RestaurantOrdering.Models;
using Serilog;

namespace RestaurantOrdering
{
    public static class TestImageStorage
    {
        public static async Task TestDirectDatabaseStorage(ApplicationDbContext context)
        {
            try
            {
                Log.Information("🧪 TEST: Starting direct database storage test...");
                
                // Create a simple Base64 test string
                var testBase64 = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/2wCETestData";
                
                var testMenuItem = new MenuItem
                {
                    Name = "Test Item Database Storage",
                    Description = "Test for database storage",
                    Price = 100.00m,
                    ImageUrl = "", // Explicitly set to empty
                    ImageData = testBase64, // Set Base64 data
                    IsAvailable = true,
                    MenuId = 1, // Ana Yemekler
                    CreatedAt = DateTime.UtcNow
                };
                
                Log.Information($"🧪 TEST: Before save - ImageUrl: '{testMenuItem.ImageUrl}'");
                Log.Information($"🧪 TEST: Before save - ImageData: '{testMenuItem.ImageData?.Substring(0, Math.Min(50, testMenuItem.ImageData.Length))}...'");
                
                context.MenuItems.Add(testMenuItem);
                await context.SaveChangesAsync();
                
                Log.Information($"🧪 TEST: ✅ Successfully saved test item with ID: {testMenuItem.Id}");
                
                // Verify by reading back
                var savedItem = await context.MenuItems.FindAsync(testMenuItem.Id);
                if (savedItem != null)
                {
                    Log.Information($"🧪 TEST: Verified - ImageUrl: '{savedItem.ImageUrl}'");
                    Log.Information($"🧪 TEST: Verified - ImageData: '{savedItem.ImageData?.Substring(0, Math.Min(50, savedItem.ImageData.Length))}...'");
                }
                
                Log.Information("🧪 TEST: Direct database storage test completed successfully");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "🧪 TEST: ❌ Test failed: {Message}", ex.Message);
            }
        }
    }
}
