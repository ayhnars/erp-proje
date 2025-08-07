using System.Collections.Generic;
using System.Linq;
using erpapi.Models; // Order sınıfı buradaysa bu kesin olmalı
using Repository; // RepositoryContext için
// using erpapi.Repository; // BUNU KALDIR! Çünkü aynı isimde class olabilir

namespace erpapi.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly RepositoryContext _context;

        public OrderRepository(RepositoryContext context)
        {
            _context = context;
        }

        public IEnumerable<erpapi.Models.Order> GetAllOrders()
        {
            return _context.Orders.ToList();
        }

        public void CreateOrder(erpapi.Models.Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void CreateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Order> IOrderRepository.GetAllOrders()
        {
            throw new NotImplementedException();
        }
    }
}
