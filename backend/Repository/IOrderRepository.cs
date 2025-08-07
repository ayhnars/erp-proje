using System.Collections.Generic;
using erpapi.Models;

namespace erpapi.Repository
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAllOrders();
        void CreateOrder(Order order);
    }
}
