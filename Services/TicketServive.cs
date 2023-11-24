using Test_Mandiri.DTO;
using Test_Mandiri.IService;

namespace Test_Mandiri.Services;
public class TicketService : ITicketService
{
    private readonly DataContext _context;
    public TicketService(DataContext dataContext)
    {
        _context = dataContext;
    }
    public Tickets GetById(Guid Id) // Get One
    {
        try
        {
            var data = _context.tickets.FirstOrDefault(d => d.TicketId.Equals(Id));
            return data;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
    }

    public List<Tickets> GetAll() // Get All
    {
        try
        {
            var data = _context.tickets.ToList();
            return data;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
    }

    public bool RemoveById(Guid Id) // Remove by Id
    {
        try
        {
            var data = _context.tickets.FirstOrDefault(d => d.TicketId.Equals(Id));
            if (data == null)
            {
                throw new Exception("Ticket Id Not Found");
            }
            _context.Remove(data);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
    }

    public List<Tickets> FindByContains(string request) // Search
    {
        try
        {
            var data = _context.tickets
            .Where(e => e.TicketId.ToString().ToLower().Contains(request.ToLower()) || e.EventName.ToString().ToLower().Contains(request.ToLower())).ToList();
            return data;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
    }

    public Tickets AddTicket(TicketDto request) // add new Ticket
    {
        try
        {
            Tickets Ticket = new Tickets
            {
                TicketId = Guid.NewGuid(),
                EventName = request.EventName,
                EventDate = request.EventDate,
                AvailableQuantity = request.AvailableQuantity,
                Price = request.Price,
                CreatedBy = "System",
                CreatedAt = DateTime.UtcNow
            };
            _context.tickets.Add(Ticket);
            _context.SaveChanges();
            return Ticket;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
    }

    public Tickets UpdateTicket(TicketDto request) // update
    {
        try
        {
            var data = _context.tickets.FirstOrDefault(d => d.TicketId == request.TicketId);
            if (data != null)
            {
                data.EventDate = request.EventDate;
                data.Price = request.Price;
                data.AvailableQuantity = request.AvailableQuantity;
                data.UpdatedAt = DateTime.UtcNow;
                data.UpdatedBy = "system";
                _context.SaveChangesAsync();

                return data;
            }
            else
            {
                throw new Exception("Ticket Id Not Found");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
    }

}