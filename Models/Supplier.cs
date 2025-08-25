using System.ComponentModel.DataAnnotations;

namespace RestaurantOrdering.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(200)]
        public string? Address { get; set; }
        
        [StringLength(20)]
        public string? Phone { get; set; }
        
        [StringLength(100)]
        public string? Email { get; set; }
        
        [StringLength(50)]
        public string? ContactPerson { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        // Navigation Properties
        public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
    }
}
