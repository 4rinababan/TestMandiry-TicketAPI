using System.ComponentModel.DataAnnotations.Schema;

namespace Test_Mandiri;
[Table("tickets")]
public class Tickets
{
    [Column("ticket_id")]
    public Guid TicketId { get; set; }
    [Column("event_name")]
    public string EventName { get; set; }
    [Column("event_date")]
    public DateTime EventDate { get; set; }
    [Column("price")]
    public decimal Price { get; set; }
    [Column("available_quantity")]
    public int AvailableQuantity { get; set; }
    [Column("created_by")]
    public string CreatedBy { get; set; }
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    [Column("updated_by")]
    public string? UpdatedBy { get; set; }
    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}

