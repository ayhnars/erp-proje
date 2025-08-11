using System.Collections.Generic;

namespace Repository.Contrats
{
    public interface IOrderRepository
    {
        IEnumerable<global::Entities.Models.Order> GetAllOrders();
        void CreateOrder(global::Entities.Models.Order order);
    }
}
