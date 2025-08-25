using System.ComponentModel.DataAnnotations;

namespace RestaurantOrdering.Models
{
    public class StockMovement
    {
        public int Id { get; set; }
        
        [Required]
        public int StockId { get; set; }
        
        [Required]
        public StockMovementType Type { get; set; }
        
        [Required]
        [Range(0.001, double.MaxValue)]
        public decimal Quantity { get; set; }
        
        [StringLength(500)]
        public string? Notes { get; set; }
        
        [StringLength(100)]
        public string? Reference { get; set; } // Fatura no, sipariş no vs.
        
        [Range(0, double.MaxValue)]
        public decimal? UnitPrice { get; set; }
        
        [StringLength(100)]
        public string? Supplier { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        [StringLength(100)]
        public string CreatedBy { get; set; } = "System";
        
        // Navigation Properties
        public virtual Stock Stock { get; set; } = null!;
        
        // Computed Properties
        public decimal TotalValue => UnitPrice.HasValue ? Quantity * UnitPrice.Value : 0;
        public string TypeText => Type.ToString();
        public string TypeCssClass => Type switch
        {
            StockMovementType.StockIn => "text-green-600 bg-green-100",
            StockMovementType.StockOut => "text-red-600 bg-red-100",
            StockMovementType.Adjustment => "text-blue-600 bg-blue-100",
            StockMovementType.Waste => "text-orange-600 bg-orange-100",
            _ => "text-gray-600 bg-gray-100"
        };
    }

    public enum StockMovementType
    {
        [Display(Name = "Stok Girişi")]
        StockIn = 1,
        
        [Display(Name = "Stok Çıkışı")]
        StockOut = 2,
        
        [Display(Name = "Stok Düzeltme")]
        Adjustment = 3,
        
        [Display(Name = "Fire/Kayıp")]
        Waste = 4
    }
}
