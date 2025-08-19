using System.Collections.Generic;
using System.Linq;
<<<<<<< HEAD
using erpapi.Models; // Order sınıfı buradaysa bu kesin olmalı
using Repository; // RepositoryContext için
// using erpapi.Repository; // BUNU KALDIR! Çünkü aynı isimde class olabilir

namespace erpapi.Repository
=======
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository.Contrats;

namespace Repository
>>>>>>> order
{
    public class OrderRepository : IOrderRepository
    {
        private readonly RepositoryContext _context;

<<<<<<< HEAD
        public OrderRepository(RepositoryContext context)
        {
            _context = context;
        }

        public IEnumerable<erpapi.Models.Order> GetAllOrders()
        {
            return _context.Orders.ToList();
        }

        public void CreateOrder(erpapi.Models.Order order)
=======
        public OrderRepository(RepositoryContext context) => _context = context;

        public IEnumerable<Entities.Models.Order> GetAllOrders()
            => _context.Orders.ToList();

        public void CreateOrder(Entities.Models.Order order)
>>>>>>> order
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

<<<<<<< HEAD
        public void CreateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Order> IOrderRepository.GetAllOrders()
        {
            throw new NotImplementedException();
=======
        public Task<Entities.Models.Order?> GetByIdAsync(int id)
            => _context.Orders
                       .AsNoTracking()
                       .FirstOrDefaultAsync(o => o.OrderID == id);

        public async Task UpdateAsync(Entities.Models.Order entity)
        {
            _context.Orders.Update(entity);
            await _context.SaveChangesAsync();
>>>>>>> order
        }
    }
}
