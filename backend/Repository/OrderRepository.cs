using System.Collections.Generic;
using System.Linq;
using Repository.Contrats;

namespace Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly RepositoryContext _context;

        public OrderRepository(RepositoryContext context) => _context = context;

        public IEnumerable<global::Entities.Models.Order> GetAllOrders()
            => _context.Orders.ToList();

        public void CreateOrder(global::Entities.Models.Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
    }
}
