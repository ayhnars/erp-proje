using System.Collections.Generic;
using OrderEntity = Entities.Models.Order;

namespace Services.Contrats
{
    public interface IOrderService
    {
        IEnumerable<OrderEntity> GetAllOrders();
        void CreateOrder(OrderEntity order);
    }
}
