using System.ComponentModel.DataAnnotations.Schema;

namespace Test_Mandiri;
[Table("orders")]
public class Orders
{

    [Column("order_id")]
    public Guid OrderId { get; set; }
    [Column("user_id")]
    public Guid UserId { get; set; }
    [Column("ticket_id")]
    public Guid TicketId { get; set; }
    [Column("quantity")]
    public int Quantity { get; set; }
    [Column("total_amount")]
    public decimal TotalAmount { get; set; }
    [Column("payment_method")]
    public decimal PaymentMethod { get; set; }
    [Column("order_date")]
    public DateTime OrderDate { get; set; }
    [Column("created_by")]
    public string CreatedBy { get; set; }
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    [Column("updated_by")]
    public string? UpdatedBy { get; set; }
    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}