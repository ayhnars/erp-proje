using Entities.Dtos.PaymentDtos;

namespace Services.Contrats
{
    public interface IPaymentManager
    {
        Task<PaymentDto> CreateAsync(PaymentForCreateDto dto);
        Task<PaymentDto?> GetAsync(int id);
        Task<IEnumerable<PaymentDto>> GetByCartAsync(int cartId);
        Task UpdateStatusAsync(int id, string newStatus);
    }
}
