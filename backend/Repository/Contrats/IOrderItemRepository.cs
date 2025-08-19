using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Repository.Contrats
{
    public interface IOrderItemRepository
    {
        // Var olanlar
        Task<IEnumerable<OrderItem>> FindAllAsync();
        Task<OrderItem?> FindByIdAsync(int id);
        Task CreateAsync(OrderItem item);
        Task UpdateAsync(OrderItem item);
        Task DeleteAsync(OrderItem item);

        // Ek imzalar (Detay/Güncelle için)
        Task<IEnumerable<OrderItem>> GetByOrderIdAsync(int orderId);
        Task<OrderItem?> GetByIdAsync(int id);
        Task InsertAsync(OrderItem entity);
        Task DeleteAsync(int id);
    }
}
