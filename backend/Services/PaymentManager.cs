using AutoMapper;
using Entities.Dtos.PaymentDtos;
using Entities.Models;
using Repository.Contrats;
using Services.Contrats;

namespace Services
{
    public class PaymentManager : IPaymentManager
    {
        private readonly IPaymentRepository _repo;
        private readonly IMapper _mapper;

        public PaymentManager(IPaymentRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PaymentDto> CreateAsync(PaymentForCreateDto dto)
        {
            var entity = _mapper.Map<Payment>(dto);
            await _repo.CreateAsync(entity);
            await _repo.SaveAsync();
            return _mapper.Map<PaymentDto>(entity);
        }

        public async Task<PaymentDto?> GetAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity is null ? null : _mapper.Map<PaymentDto>(entity);
        }

        public async Task<IEnumerable<PaymentDto>> GetByCartAsync(int cartId)
        {
            var list = await _repo.GetByCartAsync(cartId);
            return _mapper.Map<IEnumerable<PaymentDto>>(list);
        }

        public async Task UpdateStatusAsync(int id, string newStatus)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is null) return;

            entity.PaymentStatus = Enum.Parse<PaymentStatus>(newStatus, true);
            _repo.Update(entity);
            await _repo.SaveAsync();
        }
    }
}
