using System.Collections.Generic;
using Repository;

namespace erpapi.Repository
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAllOrders();
        void CreateOrder(Order order);
    }
}
