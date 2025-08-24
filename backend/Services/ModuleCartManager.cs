using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Repository.Contrats;
using Services.Contrats;

namespace Services
{
    public class ModuleCartManager : IModuleCartManager
    {
        private readonly IModuleCartRepository _repo;
        private readonly IMapper _mapper;

        public ModuleCartManager(IModuleCartRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ModuleCartDto> CreateAsync(ModuleCartForCreateDto dto)
        {
            var entity = new ModuleCart
            {
                UserId = dto.UserId,
                CompanyID = dto.CompanyID,
                TotalPrice = dto.TotalPrice,
                Status = Enum.Parse<CartStatus>(dto.Status, ignoreCase: true),
                CreatedAt = DateTime.UtcNow
            };

            await _repo.CreateAsync(entity);
            await _repo.SaveAsync();

            return _mapper.Map<ModuleCartDto>(entity);
        }

        public async Task<ModuleCartDto?> GetAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity is null ? null : _mapper.Map<ModuleCartDto>(entity);
        }

        public async Task<IEnumerable<ModuleCartDto>> GetByUserAsync(string userId)
        {
            var list = await _repo.GetByUserAsync(userId);
            return _mapper.Map<IEnumerable<ModuleCartDto>>(list);
        }

        public async Task UpdateAsync(int id, ModuleCartForUpdateDto dto)
        {
            var entity = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("Cart not found");
            entity.TotalPrice = dto.TotalPrice;
            entity.Status = Enum.Parse<CartStatus>(dto.Status, ignoreCase: true);

            _repo.Update(entity);
            await _repo.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("Cart not found");
            _repo.Delete(entity);
            await _repo.SaveAsync();
        }
    }
}
