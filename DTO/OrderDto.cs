namespace Test_Mandiri.DTO;
public class OrderDto
{
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
    public Guid TicketId { get; set; }
    public int Quantity { get; set; }
    public decimal PaymentMethod { get; set; }
}