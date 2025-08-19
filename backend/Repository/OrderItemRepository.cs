using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Repository.Contrats;

namespace Repository
{
    public class OrderItemRepository : RepositoryBase<OrderItem>, IOrderItemRepository
    {
        private readonly RepositoryContext _context;

        public OrderItemRepository(RepositoryContext context) : base(context)
        {
            _context = context;
        }

        // --- Interface'in beklediği metot ---
        public async Task CreateAsync(OrderItem item)
        {
            await _context.Set<OrderItem>().AddAsync(item);
            await _context.SaveChangesAsync();
        }

        // (İstersen kalsın) Hepsini listeleyen eski metot
        public async Task<IEnumerable<OrderItem>> FindAllAsync()
            => await _context.Set<OrderItem>()
                             .AsNoTracking()
                             .ToListAsync();

        // (İstersen kalsın) Eski tekil bulma
        public async Task<OrderItem?> FindByIdAsync(int id)
            => await _context.Set<OrderItem>().FindAsync(id);

        // Belirli bir siparişin kalemleri
        public async Task<IEnumerable<OrderItem>> GetByOrderIdAsync(int orderId)
            => await _context.Set<OrderItem>()
                             .AsNoTracking()
                             .Where(i => i.OrderID == orderId)
                             .ToListAsync();

        // Tekil getir (AsNoTracking ile)
        public Task<OrderItem?> GetByIdAsync(int id)
            => _context.Set<OrderItem>()
                       .AsNoTracking()
                       .FirstOrDefaultAsync(x => x.ItemID == id);

        // (Önceden yazdığın) Ekle - dilersen kullanmaya devam et
        public async Task InsertAsync(OrderItem item)
        {
            _context.Set<OrderItem>().Add(item);
            await _context.SaveChangesAsync();
        }

        // Güncelle
        public async Task UpdateAsync(OrderItem item)
        {
            _context.Set<OrderItem>().Update(item);
            await _context.SaveChangesAsync();
        }

        // Sil (id ile)
        public async Task DeleteAsync(int id)
        {
            var ent = await _context.Set<OrderItem>().FindAsync(id);
            if (ent == null) return;
            _context.Set<OrderItem>().Remove(ent);
            await _context.SaveChangesAsync();
        }

        // (İstersen kalsın) Eski imza: entity vererek silme
        public async Task DeleteAsync(OrderItem item)
        {
            _context.Set<OrderItem>().Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
