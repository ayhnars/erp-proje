using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Contrats
{
    public interface IOrderRepository
    {
        IEnumerable<Entities.Models.Order> GetAllOrders();
        void CreateOrder(Entities.Models.Order order);
        Task<Entities.Models.Order?> GetByIdAsync(int id);
        Task UpdateAsync(Entities.Models.Order entity);
    }
}
