using System.Collections.Generic;
using Entities.Models;

namespace Services.Contracts
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAllOrders();
        void CreateOrder(Order order);
    }
}
