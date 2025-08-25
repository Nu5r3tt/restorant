using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantOrdering.Models
{
    public enum PaymentType
    {
        Cash = 0,
        Card = 1,
        Mixed = 2
    }

    public enum PaymentStatus
    {
        Pending = 0,
        Processing = 1,
        Completed = 2,
        Failed = 3,
        Cancelled = 4,
        Refunded = 5
    }

    public class Payment
    {
        public int Id { get; set; }
        
        [Required]
        public int TableId { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal? CashAmount { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal? CardAmount { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal? CashReceived { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Change { get; set; }
        
        [Required]
        public PaymentType PaymentType { get; set; }
        
        [Required]
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? CompletedAt { get; set; }
        
        [StringLength(100)]
        public string? TransactionId { get; set; }
        
        [StringLength(50)]
        public string? PosTerminalId { get; set; }
        
        [StringLength(500)]
        public string? Notes { get; set; }
        
        [StringLength(100)]
        public string? CashierName { get; set; }
        
        // Navigation properties
        [ForeignKey("TableId")]
        public virtual Table Table { get; set; } = null!;
        
        public virtual ICollection<PaymentItem> PaymentItems { get; set; } = new List<PaymentItem>();
    }

    public class PaymentItem
    {
        public int Id { get; set; }
        
        [Required]
        public int PaymentId { get; set; }
        
        [Required]
        public int OrderId { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        
        // Navigation properties
        [ForeignKey("PaymentId")]
        public virtual Payment Payment { get; set; } = null!;
        
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; } = null!;
    }

    // POS Integration Models
    public class PosRequest
    {
        public decimal Amount { get; set; }
        public string TransactionId { get; set; } = string.Empty;
        public string TerminalId { get; set; } = string.Empty;
        public DateTime RequestTime { get; set; } = DateTime.UtcNow;
    }

    public class PosResponse
    {
        public bool Success { get; set; }
        public string TransactionId { get; set; } = string.Empty;
        public string ApprovalCode { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
        public DateTime ResponseTime { get; set; } = DateTime.UtcNow;
    }
}
