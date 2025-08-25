using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantOrdering.Models
{
    public enum OrderStatus
    {
        Pending = 0,
        Confirmed = 1,
        Preparing = 2,
        Ready = 3,
        Served = 4,
        Cancelled = 5
    }

    public class Order
    {
        public int Id { get; set; }
        
        [Required]
        public int TableId { get; set; }
        
        [Required]
        public int MenuItemId { get; set; }
        
        [Required]
        [Range(1, 100)]
        public int Quantity { get; set; }
        
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
        
        [StringLength(500)]
        public string? Notes { get; set; }
        
        // Navigation properties
        [ForeignKey("TableId")]
        public virtual Table Table { get; set; } = null!;
        
        [ForeignKey("MenuItemId")]
        public virtual MenuItem MenuItem { get; set; } = null!;
    }
}
