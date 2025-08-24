// Repository/OrderRepository.cs
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository.Contrats;

// Kolaylık için alias
using OrderEntity = Entities.Models.Order;

namespace Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly RepositoryContext _context;

        public OrderRepository(RepositoryContext context) => _context = context;

        public IEnumerable<OrderEntity> GetAllOrders()
            => _context.Orders.AsNoTracking().ToList();

        public void CreateOrder(OrderEntity order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public Task<OrderEntity?> GetByIdAsync(int id)
            => _context.Orders.AsNoTracking()
                              .FirstOrDefaultAsync(o => o.OrderID == id);

        public async Task UpdateAsync(OrderEntity entity)
        {
            _context.Orders.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
