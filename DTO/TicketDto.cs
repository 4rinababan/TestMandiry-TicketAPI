namespace Test_Mandiri.DTO;
public class TicketDto
{
    public Guid TicketId { get; set; }
    public string EventName { get; set; }
    public DateTime EventDate { get; set; }
    public decimal Price { get; set; }
    public int AvailableQuantity { get; set; }
}