using Test_Mandiri.DTO;

namespace Test_Mandiri.IService;
public interface IOrderService
{
    Orders GetById(Guid Id);
    List<Orders> GetAll();
    bool RemoveById(Guid Id);
    List<Orders> FindByContains(string request);
    Orders AddOrder(OrderDto request);
    Orders UpdateOrders(OrderDto request);
}