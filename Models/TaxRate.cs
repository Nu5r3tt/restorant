using System.ComponentModel.DataAnnotations;

namespace RestaurantOrdering.Models
{
    public class TaxRate
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [Range(0, 100)]
        public decimal Rate { get; set; }
        
        [StringLength(200)]
        public string? Description { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        // Navigation Properties
        public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
    }
}
