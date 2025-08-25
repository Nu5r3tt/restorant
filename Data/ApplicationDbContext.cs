using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestaurantOrdering.Models;

namespace RestaurantOrdering.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentItem> PaymentItems { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockMovement> StockMovements { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<TaxRate> TaxRates { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure decimal precision
            builder.Entity<MenuItem>()
                .Property(m => m.Price)
                .HasPrecision(18, 2);

            builder.Entity<Order>()
                .Property(o => o.TotalPrice)
                .HasPrecision(18, 2);

            // Payment decimal properties
            builder.Entity<Payment>()
                .Property(p => p.TotalAmount)
                .HasPrecision(18, 2);

            builder.Entity<Payment>()
                .Property(p => p.CashAmount)
                .HasPrecision(18, 2);

            builder.Entity<Payment>()
                .Property(p => p.CardAmount)
                .HasPrecision(18, 2);

            builder.Entity<Payment>()
                .Property(p => p.CashReceived)
                .HasPrecision(18, 2);

            builder.Entity<Payment>()
                .Property(p => p.Change)
                .HasPrecision(18, 2);

            builder.Entity<PaymentItem>()
                .Property(pi => pi.Amount)
                .HasPrecision(18, 2);

            // Stock decimal properties
            builder.Entity<Stock>()
                .Property(s => s.CurrentQuantity)
                .HasPrecision(18, 3);

            builder.Entity<Stock>()
                .Property(s => s.MinimumQuantity)
                .HasPrecision(18, 3);

            builder.Entity<Stock>()
                .Property(s => s.PurchasePrice)
                .HasPrecision(18, 2);

            builder.Entity<StockMovement>()
                .Property(sm => sm.Quantity)
                .HasPrecision(18, 3);

            builder.Entity<StockMovement>()
                .Property(sm => sm.UnitPrice)
                .HasPrecision(18, 2);

            // Configure relationships
            builder.Entity<MenuItem>()
                .HasOne(mi => mi.Menu)
                .WithMany(m => m.MenuItems)
                .HasForeignKey(mi => mi.MenuId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Order>()
                .HasOne(o => o.Table)
                .WithMany(t => t.Orders)
                .HasForeignKey(o => o.TableId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Order>()
                .HasOne(o => o.MenuItem)
                .WithMany(mi => mi.Orders)
                .HasForeignKey(o => o.MenuItemId)
                .OnDelete(DeleteBehavior.Restrict);

            // Payment relationships
            builder.Entity<Payment>()
                .HasOne(p => p.Table)
                .WithMany()
                .HasForeignKey(p => p.TableId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<PaymentItem>()
                .HasOne(pi => pi.Payment)
                .WithMany(p => p.PaymentItems)
                .HasForeignKey(pi => pi.PaymentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<PaymentItem>()
                .HasOne(pi => pi.Order)
                .WithMany()
                .HasForeignKey(pi => pi.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            // Stock relationships
            builder.Entity<StockMovement>()
                .HasOne(sm => sm.Stock)
                .WithMany(s => s.StockMovements)
                .HasForeignKey(sm => sm.StockId)
                .OnDelete(DeleteBehavior.Cascade);

            // Stock - Supplier relationship
            builder.Entity<Stock>()
                .HasOne(s => s.Supplier)
                .WithMany(sup => sup.Stocks)
                .HasForeignKey(s => s.SupplierId)
                .OnDelete(DeleteBehavior.SetNull);

            // Stock - TaxRate relationship
            builder.Entity<Stock>()
                .HasOne(s => s.TaxRate)
                .WithMany(tr => tr.Stocks)
                .HasForeignKey(s => s.TaxRateId)
                .OnDelete(DeleteBehavior.SetNull);

            // Configure decimal precision for new models
            builder.Entity<TaxRate>()
                .Property(tr => tr.Rate)
                .HasPrecision(5, 2);

            // Unique constraints
            builder.Entity<Table>()
                .HasIndex(t => t.TableNumber)
                .IsUnique();

            // Seed data
            SeedData(builder);
        }

        private static void SeedData(ModelBuilder builder)
        {
            var seedDate = new DateTime(2025, 8, 21, 18, 0, 0, DateTimeKind.Utc);

            // Seed Tables
            builder.Entity<Table>().HasData(
                new Table { Id = 1, TableNumber = 1, IsActive = true, CreatedAt = seedDate },
                new Table { Id = 2, TableNumber = 2, IsActive = true, CreatedAt = seedDate },
                new Table { Id = 3, TableNumber = 3, IsActive = true, CreatedAt = seedDate },
                new Table { Id = 4, TableNumber = 4, IsActive = true, CreatedAt = seedDate },
                new Table { Id = 5, TableNumber = 5, IsActive = true, CreatedAt = seedDate }
            );

            // Seed Menus
            builder.Entity<Menu>().HasData(
                new Menu { Id = 1, Name = "Ana Yemekler", Description = "Restoran ana yemek menüsü", IsActive = true, CreatedAt = seedDate },
                new Menu { Id = 2, Name = "Başlangıçlar", Description = "Başlangıç yemekleri", IsActive = true, CreatedAt = seedDate },
                new Menu { Id = 3, Name = "İçecekler", Description = "Soğuk ve sıcak içecekler", IsActive = true, CreatedAt = seedDate },
                new Menu { Id = 4, Name = "Tatlılar", Description = "Ev yapımı tatlılar", IsActive = true, CreatedAt = seedDate }
            );

            // Seed Menu Items
            builder.Entity<MenuItem>().HasData(
                // Ana Yemekler
                new MenuItem { Id = 1, MenuId = 1, Name = "Köfte", Description = "Ev yapımı köfte, patates kızartması ile", Price = 85.00m, IsAvailable = true, CreatedAt = seedDate },
                new MenuItem { Id = 2, MenuId = 1, Name = "Tavuk Şiş", Description = "Izgara tavuk şiş, pilav ve salata ile", Price = 75.00m, IsAvailable = true, CreatedAt = seedDate },
                new MenuItem { Id = 3, MenuId = 1, Name = "Adana Kebap", Description = "Acılı adana kebap, bulgur pilav ile", Price = 95.00m, IsAvailable = true, CreatedAt = seedDate },
                
                // Başlangıçlar
                new MenuItem { Id = 4, MenuId = 2, Name = "Mercimek Çorbası", Description = "Geleneksel mercimek çorbası", Price = 25.00m, IsAvailable = true, CreatedAt = seedDate },
                new MenuItem { Id = 5, MenuId = 2, Name = "Çoban Salata", Description = "Taze sebzelerle çoban salata", Price = 30.00m, IsAvailable = true, CreatedAt = seedDate },
                new MenuItem { Id = 6, MenuId = 2, Name = "Humus", Description = "Ev yapımı humus", Price = 35.00m, IsAvailable = true, CreatedAt = seedDate },
                
                // İçecekler
                new MenuItem { Id = 7, MenuId = 3, Name = "Çay", Description = "Türk çayı", Price = 8.00m, IsAvailable = true, CreatedAt = seedDate },
                new MenuItem { Id = 8, MenuId = 3, Name = "Türk Kahvesi", Description = "Geleneksel türk kahvesi", Price = 15.00m, IsAvailable = true, CreatedAt = seedDate },
                new MenuItem { Id = 9, MenuId = 3, Name = "Ayran", Description = "Soğuk ayran", Price = 12.00m, IsAvailable = true, CreatedAt = seedDate },
                
                // Tatlılar
                new MenuItem { Id = 10, MenuId = 4, Name = "Baklava", Description = "Antep fıstıklı baklava", Price = 45.00m, IsAvailable = true, CreatedAt = seedDate },
                new MenuItem { Id = 11, MenuId = 4, Name = "Sütlaç", Description = "Ev yapımı sütlaç", Price = 25.00m, IsAvailable = true, CreatedAt = seedDate },
                new MenuItem { Id = 12, MenuId = 4, Name = "Künefe", Description = "Sıcak künefe", Price = 50.00m, IsAvailable = true, CreatedAt = seedDate }
            );

            // Seed Suppliers
            builder.Entity<Supplier>().HasData(
                new Supplier { Id = 1, Name = "Metro Market", Address = "İstanbul, Türkiye", Phone = "0212 555 0001", Email = "info@metro.com", ContactPerson = "Ahmet Yılmaz", IsActive = true, CreatedAt = seedDate },
                new Supplier { Id = 2, Name = "Carrefour", Address = "Ankara, Türkiye", Phone = "0312 555 0002", Email = "tedarik@carrefour.com", ContactPerson = "Fatma Kaya", IsActive = true, CreatedAt = seedDate },
                new Supplier { Id = 3, Name = "BİM", Address = "İzmir, Türkiye", Phone = "0232 555 0003", Email = "satinalma@bim.com", ContactPerson = "Mehmet Öz", IsActive = true, CreatedAt = seedDate },
                new Supplier { Id = 4, Name = "Migros", Address = "Bursa, Türkiye", Phone = "0224 555 0004", Email = "tedarikci@migros.com", ContactPerson = "Ayşe Demir", IsActive = true, CreatedAt = seedDate },
                new Supplier { Id = 5, Name = "Yerel Market", Address = "Antalya, Türkiye", Phone = "0242 555 0005", Email = "info@yerelmarket.com", ContactPerson = "Ali Vural", IsActive = true, CreatedAt = seedDate }
            );

            // Seed Tax Rates
            builder.Entity<TaxRate>().HasData(
                new TaxRate { Id = 1, Name = "KDV %1", Rate = 1.00m, Description = "Temel gıda maddeleri için KDV oranı", IsActive = true, CreatedAt = seedDate },
                new TaxRate { Id = 2, Name = "KDV %8", Rate = 8.00m, Description = "Bazı gıda maddeleri için KDV oranı", IsActive = true, CreatedAt = seedDate },
                new TaxRate { Id = 3, Name = "KDV %10", Rate = 10.00m, Description = "İndirimli KDV oranı", IsActive = true, CreatedAt = seedDate },
                new TaxRate { Id = 4, Name = "KDV %18", Rate = 18.00m, Description = "Genel KDV oranı", IsActive = true, CreatedAt = seedDate },
                new TaxRate { Id = 5, Name = "KDV %20", Rate = 20.00m, Description = "Lüks ürünler için KDV oranı", IsActive = true, CreatedAt = seedDate }
            );
        }
    }
}
