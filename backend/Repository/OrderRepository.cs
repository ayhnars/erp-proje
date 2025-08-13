using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository.Contrats;

namespace Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly RepositoryContext _context;

        public OrderRepository(RepositoryContext context) => _context = context;

        public IEnumerable<Entities.Models.Order> GetAllOrders()
            => _context.Orders.ToList();

        public void CreateOrder(Entities.Models.Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public Task<Entities.Models.Order?> GetByIdAsync(int id)
            => _context.Orders
                       .AsNoTracking()
                       .FirstOrDefaultAsync(o => o.OrderID == id);

        public async Task UpdateAsync(Entities.Models.Order entity)
        {
            _context.Orders.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
