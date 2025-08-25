using System.ComponentModel.DataAnnotations;

namespace RestaurantOrdering.Models
{
    public class Stock
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        public StockCategory Category { get; set; }
        
        [Required]
        [Range(0, double.MaxValue)]
        public decimal CurrentQuantity { get; set; }
        
        [Required]
        [Range(0, double.MaxValue)]
        public decimal MinimumQuantity { get; set; }
        
        [Required]
        [StringLength(20)]
        public string Unit { get; set; } = string.Empty; // kg, adet, litre, gram vs.
        
        [Range(0, double.MaxValue)]
        public decimal? PurchasePrice { get; set; }
        
        // Foreign Keys
        public int? SupplierId { get; set; }
        public int? TaxRateId { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? LastUpdated { get; set; }
        public bool IsActive { get; set; } = true;
        
        // Navigation Properties
        public virtual Supplier? Supplier { get; set; }
        public virtual TaxRate? TaxRate { get; set; }
        public virtual ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();
        
        // Computed Properties
        public bool IsLowStock => CurrentQuantity <= MinimumQuantity;
        public string StatusText => IsLowStock ? "Düşük Stok" : "Normal";
        public string StatusCssClass => IsLowStock ? "text-red-600 bg-red-100" : "text-green-600 bg-green-100";
    }

    public enum StockCategory
    {
        [Display(Name = "Et ve Et Ürünleri")]
        Meat = 1,
        
        [Display(Name = "Sebze ve Meyve")]
        Vegetables = 2,
        
        [Display(Name = "Süt Ürünleri")]
        Dairy = 3,
        
        [Display(Name = "Tahıllar ve Bakliyat")]
        Grains = 4,
        
        [Display(Name = "Baharat ve Çeşni")]
        Spices = 5,
        
        [Display(Name = "İçecek")]
        Beverages = 6,
        
        [Display(Name = "Temizlik Malzemeleri")]
        Cleaning = 7,
        
        [Display(Name = "Ambalaj Malzemeleri")]
        Packaging = 8,
        
        [Display(Name = "Diğer")]
        Other = 9
    }
}
