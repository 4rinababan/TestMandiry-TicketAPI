using Test_Mandiri.DTO;
using Test_Mandiri.IService;

namespace Test_Mandiri.Services;
public class OrderService : IOrderService
{
    private readonly DataContext _context;
    public OrderService(DataContext dataContext)
    {
        _context=dataContext;
    }
    public Orders GetById(Guid Id) // Get One
    {
        try
        {
            var data = _context.orders.FirstOrDefault(d => d.OrderId.Equals(Id));
            return data;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
    }

    public List<Orders> GetAll() // Get All
    {
        try
        {
            var data = _context.orders.ToList();
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
            var data = _context.orders.FirstOrDefault(d => d.OrderId.Equals(Id));
            if (data == null)
            {
                throw new Exception("Order Id Not Found");
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

    public List<Orders> FindByContains(string request) // Search
    {
        try
        {
            var data = _context.orders
            .Where(e => e.OrderId.ToString().ToLower().Contains(request.ToLower()) || e.UserId.ToString().ToLower().Contains(request.ToLower())
            || e.UserId.ToString().ToLower().Contains(request.ToLower()) || e.PaymentMethod.ToString().ToLower().Contains(request.ToLower())).ToList();
            return data;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
    }

    public Orders AddOrder(OrderDto request) // add new order
    {
        try
        {
            Orders order = new Orders
            {
                OrderId = Guid.NewGuid(),
                UserId = request.UserId,
                TicketId = request.TicketId,
                Quantity = request.Quantity,
                PaymentMethod = request.PaymentMethod,
                TotalAmount =request.Quantity * GetTicketPrice(request.TicketId),
                OrderDate = DateTime.UtcNow,
                CreatedBy = "System",
                CreatedAt = DateTime.UtcNow
            };
            _context.orders.Add(order);
            _context.SaveChanges();
            return order;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
    }

    public Orders UpdateOrders(OrderDto request)
    {
        try
        {
            var data = _context.orders.FirstOrDefault(d => d.OrderId == request.OrderId);
            if (data != null)
            {
                data.Quantity = request.Quantity;
                data.PaymentMethod = request.PaymentMethod;
                data.TotalAmount = request.Quantity * GetTicketPrice(request.TicketId);
                data.UpdatedAt = DateTime.UtcNow;
                data.UpdatedBy="system";
                _context.SaveChangesAsync();

                return data;
            }
            else
            {
                throw new Exception("Order Id Not Found");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
    }

    private decimal GetTicketPrice(Guid Id) // Get Ticket Price
    {
        try
        {
            var data = _context.tickets.FirstOrDefault(d => d.TicketId.Equals(Id));
            return data.Price;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
    }

}