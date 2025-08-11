namespace Repository.Contrats
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities.Models;

    public interface IOrderItemRepository
    {
        Task<IEnumerable<OrderItem>> FindAllAsync();
        Task<OrderItem?> FindByIdAsync(int id);
        Task CreateAsync(OrderItem item);
        Task UpdateAsync(OrderItem item);
        Task DeleteAsync(OrderItem item);
    }
}
