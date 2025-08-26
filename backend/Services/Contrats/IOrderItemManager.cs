using System;
using Entities.Models;

namespace Services.Contrats
{
    public interface IOrderItemManager
    {
        Task<IEnumerable<OrderItem>> GetAllAsync();
        Task<OrderItem> GetByIdAsync(int id);
        Task CreateAsync(OrderItem item);
        Task UpdateAsync(OrderItem item);
        Task DeleteAsync(int id);
    }
}
