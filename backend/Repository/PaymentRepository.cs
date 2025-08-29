using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Contrats;

namespace Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly RepositoryContext _context;

        public PaymentRepository(RepositoryContext context) => _context = context;

        public async Task CreateAsync(Payment entity)
        {
            await _context.Payments.AddAsync(entity);
        }

        public void Update(Payment entity)
        {
            _context.Payments.Update(entity);
        }

        public Task<Payment?> GetByIdAsync(int id)
        {
            return _context.Payments
                           .AsNoTracking()
                           .FirstOrDefaultAsync(p => p.PaymentID == id);
        }

        public async Task<IEnumerable<Payment>> GetByCartAsync(int cartId)
        {
            return await _context.Payments
                                 .AsNoTracking()
                                 .Where(p => p.CartID == cartId)
                                 .ToListAsync();
        }

        public Task SaveAsync() => _context.SaveChangesAsync();
    }
}
