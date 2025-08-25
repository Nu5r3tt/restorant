using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantOrdering.Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        
        [Required]
        public int MenuId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        
        // [StringLength(200)]  // Temporarily removed to debug
        public string? ImageUrl { get; set; }
        
        // Resim verisini Base64 formatında saklamak için
        public string? ImageData { get; set; }
        
        public bool IsAvailable { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        [ForeignKey("MenuId")]
        public virtual Menu Menu { get; set; } = null!;
        
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
