using System.ComponentModel.DataAnnotations;

namespace RestaurantOrdering.Models
{
    public class Table
    {
        public int Id { get; set; }
        
        [Required]
        public int TableNumber { get; set; }
        
        [StringLength(500)]
        public string? QrCodeUrl { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation property
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
