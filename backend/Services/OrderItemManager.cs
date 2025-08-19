using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;
using Repository.Contrats;
using Services;// IOrderItemRepository burada (dosyada da bu namespace olmalı)
using Services.Contrats;

namespace Services
{
    public class OrderItemManager : IOrderItemManager
    {
        private readonly IOrderItemRepository _repo;

        public OrderItemManager(IOrderItemRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<OrderItem>> GetAllAsync() => _repo.FindAllAsync();

        public Task<OrderItem?> GetByIdAsync(int id) => _repo.FindByIdAsync(id);

        public Task CreateAsync(OrderItem item) => _repo.CreateAsync(item);

        public Task UpdateAsync(OrderItem item) => _repo.UpdateAsync(item);

        public async Task DeleteAsync(int id)
        {
            var entity = await _repo.FindByIdAsync(id);
            if (entity != null)
                await _repo.DeleteAsync(entity);
        }
    }
}
