using Test_Mandiri.DTO;

namespace Test_Mandiri.IService;
public interface ITicketService
{
    Tickets GetById(Guid Id);
    List<Tickets> GetAll();
    bool RemoveById(Guid Id);
    List<Tickets> FindByContains(string request);
    Tickets AddTicket(TicketDto request);
    Tickets UpdateTicket(TicketDto request);
}