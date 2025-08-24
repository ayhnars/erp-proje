// Repository.Contrats/IOrderRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderEntity = Entities.Models.Order;

namespace Repository.Contrats
{
    public interface IOrderRepository
    {
        IEnumerable<OrderEntity> GetAllOrders();
        Task<OrderEntity?> GetByIdAsync(int id);
        void CreateOrder(OrderEntity order);
        Task UpdateAsync(OrderEntity entity);
    }
}
