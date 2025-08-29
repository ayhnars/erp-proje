using Entities.Models;

namespace Repository.Contrats
{
    public interface IPaymentRepository
    {
        Task CreateAsync(Payment entity);
        void Update(Payment entity);
        Task<Payment?> GetByIdAsync(int id);
        Task<IEnumerable<Payment>> GetByCartAsync(int cartId);
        Task SaveAsync();
    }
}
