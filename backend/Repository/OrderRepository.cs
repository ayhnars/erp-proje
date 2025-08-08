using System.Collections.Generic;
using System.Linq;
using erpapi.Repository;
using Repository;

namespace Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly RepositoryContext _context;

        public OrderRepository(RepositoryContext context)
        {
            _context = context;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _context.Orders.ToList();
        }

        public void CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
    }
}
