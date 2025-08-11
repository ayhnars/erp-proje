using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;     // ← EF Core
using Entities.Models;
using Repository.Contrats;               // ← IOrderItemRepository burada

namespace Repository
{
    public class OrderItemRepository : RepositoryBase<OrderItem>, IOrderItemRepository
    {
        private readonly RepositoryContext _context;

        public OrderItemRepository(RepositoryContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderItem>> FindAllAsync()
        {
            return await _context.Set<OrderItem>()
                                 .AsNoTracking()
                                 .ToListAsync();
        }

        public async Task<OrderItem?> FindByIdAsync(int id)
        {
            // Eğer PK ise FindAsync hızlıdır
            return await _context.Set<OrderItem>().FindAsync(id);
            // PK değilse şu da olur:
            // return await _context.Set<OrderItem>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreateAsync(OrderItem item)
        {
            _context.Set<OrderItem>().Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(OrderItem item)
        {
            _context.Set<OrderItem>().Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(OrderItem item)
        {
            _context.Set<OrderItem>().Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
