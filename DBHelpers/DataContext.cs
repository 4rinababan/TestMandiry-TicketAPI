using Microsoft.EntityFrameworkCore;

namespace Test_Mandiri;
public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    public DbSet<Orders> orders { get; set; } // Order Entity
    public DbSet<Tickets> tickets { get; set; } // Ticket Entity

    protected override void OnModelCreating(ModelBuilder modelBuilder) // On migrate create
    {
        base.OnModelCreating(modelBuilder);

        // add dummy data Ticketson migrate
        modelBuilder.Entity<Tickets>().HasData(
            new Tickets
            {
                TicketId = Guid.NewGuid(),
                EventName = "Coldplay Conser",
                EventDate = DateTime.UtcNow.AddDays(7),
                Price = 2500000.00m,
                AvailableQuantity = 100,
                CreatedBy = "System",
                CreatedAt = DateTime.UtcNow,
                UpdatedBy = null,
                UpdatedAt = null
            },
            new Tickets
            {
                TicketId = Guid.NewGuid(),
                EventName = "BTS Conser",
                EventDate = DateTime.UtcNow.AddDays(14),
                Price = 3000000.00m,
                AvailableQuantity = 75,
                CreatedBy = "System",
                CreatedAt = DateTime.UtcNow,
                UpdatedBy = null,
                UpdatedAt = null
            },
            new Tickets
            {
                TicketId = Guid.NewGuid(),
                EventName = "Westlife Conser",
                EventDate = DateTime.UtcNow.AddDays(11),
                Price = 1000000.00m,
                AvailableQuantity = 115,
                CreatedBy = "System",
                CreatedAt = DateTime.UtcNow,
                UpdatedBy = null,
                UpdatedAt = null
            }
        );
    }
}