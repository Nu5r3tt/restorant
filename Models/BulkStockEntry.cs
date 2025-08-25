using System.ComponentModel.DataAnnotations;

namespace RestaurantOrdering.Models
{
    public class BulkStockEntry
    {
        public List<StockEntryItem> Items { get; set; } = new List<StockEntryItem>();
    }

    public class StockEntryItem
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        public StockCategory Category { get; set; }
        
        [Required]
        [Range(0, double.MaxValue)]
        public decimal CurrentQuantity { get; set; }
        
        [Required]
        public string Unit { get; set; } = string.Empty;
        
        [Range(0, double.MaxValue)]
        public decimal? PurchasePrice { get; set; }
        
        public int? SupplierId { get; set; }
        
        public int? TaxRateId { get; set; }
        
        // Display properties
        public string? SupplierName { get; set; }
        public string? TaxRateName { get; set; }
    }

    public enum StockUnit
    {
        [Display(Name = "Kilogram (kg)")]
        Kilogram,
        
        [Display(Name = "Gram (gr)")]
        Gram,
        
        [Display(Name = "Adet")]
        Piece,
        
        [Display(Name = "Litre (lt)")]
        Liter,
        
        [Display(Name = "Mililitre (ml)")]
        Milliliter,
        
        [Display(Name = "Demet")]
        Bundle,
        
        [Display(Name = "Paket")]
        Package,
        
        [Display(Name = "Kutu")]
        Box,
        
        [Display(Name = "Şişe")]
        Bottle,
        
        [Display(Name = "Poşet")]
        Bag,
        
        [Display(Name = "Teneke")]
        Can,
        
        [Display(Name = "Çuval")]
        Sack
    }
}
