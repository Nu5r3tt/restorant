using Microsoft.EntityFrameworkCore;
using RestaurantOrdering.Data;
using RestaurantOrdering.Models;

namespace RestaurantOrdering.Services
{
    public class TestOrderService
    {
        private readonly ApplicationDbContext _context;

        public TestOrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateTestOrdersAsync()
        {
            // Masa 1 için test siparişleri
            var table1Orders = new List<Order>
            {
                new Order
                {
                    TableId = 1,
                    MenuItemId = 1, // Köfte
                    Quantity = 2,
                    Notes = "Az pişmiş",
                    Status = OrderStatus.Served,
                    OrderDate = DateTime.Now.AddHours(-1),
                    TotalPrice = 170.00m
                },
                new Order
                {
                    TableId = 1,
                    MenuItemId = 7, // Çay
                    Quantity = 2,
                    Notes = "",
                    Status = OrderStatus.Served,
                    OrderDate = DateTime.Now.AddMinutes(-30),
                    TotalPrice = 16.00m
                }
            };

            // Masa 2 için test siparişleri
            var table2Orders = new List<Order>
            {
                new Order
                {
                    TableId = 2,
                    MenuItemId = 2, // Tavuk Şiş
                    Quantity = 1,
                    Notes = "",
                    Status = OrderStatus.Served,
                    OrderDate = DateTime.Now.AddHours(-2),
                    TotalPrice = 75.00m
                },
                new Order
                {
                    TableId = 2,
                    MenuItemId = 4, // Mercimek Çorbası
                    Quantity = 1,
                    Notes = "",
                    Status = OrderStatus.Served,
                    OrderDate = DateTime.Now.AddHours(-2),
                    TotalPrice = 25.00m
                },
                new Order
                {
                    TableId = 2,
                    MenuItemId = 10, // Baklava
                    Quantity = 1,
                    Notes = "",
                    Status = OrderStatus.Served,
                    OrderDate = DateTime.Now.AddMinutes(-15),
                    TotalPrice = 45.00m
                }
            };

            // Masa 3 için test siparişleri
            var table3Orders = new List<Order>
            {
                new Order
                {
                    TableId = 3,
                    MenuItemId = 3, // Adana Kebap
                    Quantity = 1,
                    Notes = "Çok acı",
                    Status = OrderStatus.Served,
                    OrderDate = DateTime.Now.AddMinutes(-45),
                    TotalPrice = 95.00m
                },
                new Order
                {
                    TableId = 3,
                    MenuItemId = 9, // Ayran
                    Quantity = 2,
                    Notes = "",
                    Status = OrderStatus.Served,
                    OrderDate = DateTime.Now.AddMinutes(-45),
                    TotalPrice = 24.00m
                }
            };

            var allOrders = table1Orders.Concat(table2Orders).Concat(table3Orders);

            foreach (var order in allOrders)
            {
                // Eğer sipariş zaten yoksa ekle
                var existingOrder = await _context.Orders
                    .FirstOrDefaultAsync(o => o.TableId == order.TableId && 
                                            o.MenuItemId == order.MenuItemId && 
                                            o.Quantity == order.Quantity);

                if (existingOrder == null)
                {
                    _context.Orders.Add(order);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
